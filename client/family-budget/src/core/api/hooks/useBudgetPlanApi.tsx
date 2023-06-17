import {AxiosClient} from "@core/api/AxiosClient";
import {useAppContext} from "@core/context/ApplicationContext";
import type {IBudgetPlan} from "@core/models/budgetPlan";
import type {DataEnvelope} from "@core/models/dataEnvelope";
import type {IPaginatedList, IPaginationRequest} from "@core/models/pagination";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {IUserBase} from "@core/models/user";

const client = AxiosClient.initialize();
const budgetPlanUrlSegment = "budget-plans";
const key = "budget-plans";

export const useBudgetPlanApi = () => {
    const appContext = useAppContext();
    const queryClient = useQueryClient();

    const browseBudgetPlans = (pagination: IPaginationRequest) => {
        return useQuery({
            queryKey: [key, "browse", pagination.pageIndex, pagination.pageSize, pagination.isAscending],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.browse<IBudgetPlan>(`${budgetPlanUrlSegment}/browse`, pagination);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<IPaginatedList<IBudgetPlan>>) => data.data as IPaginatedList<IBudgetPlan>,
            enabled: true,
        });
    };

    const createBudgetPlan = useMutation({
        mutationKey: [key, "create"],
        mutationFn: async (budgetPlan: {
            name: string;
            description: string;
            startDate: Date;
            endDate: Date;
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post<IUserBase>(`${budgetPlanUrlSegment}/create`, budgetPlan);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => await queryClient.invalidateQueries([key, "browse"])
    });

    const addExpense = useMutation({
        mutationKey: [key, "expense", "add"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            expense: { name: string; date: Date; amount: number; expenseCategory: string; }
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post<IUserBase>(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/expense/add`, payload.expense);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => await queryClient.invalidateQueries([key, "browse"])
    });

    const addIncome = useMutation({
        mutationKey: [key, "income", "add"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            income: { name: string; date: Date; amount: number; incomeType: string; }
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post<IUserBase>(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/income/add`, payload.income);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => await queryClient.invalidateQueries([key, "browse"])
    });

    return {
        queries: {
            browseBudgetPlans
        },
        commands: {
            createBudgetPlan,
            addExpense,
            addIncome,
        }
    };
};
