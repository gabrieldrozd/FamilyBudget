import {AxiosClient} from "@core/api/AxiosClient";
import {useAppContext} from "@core/context/ApplicationContext";
import type {IBudgetPlan, IBudgetPlanDetails} from "@core/models/budgetPlan";
import type {DataEnvelope} from "@core/models/dataEnvelope";
import type {IPaginatedList, IPaginationRequest} from "@core/models/pagination";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";

const client = AxiosClient.initialize();
const budgetPlanUrlSegment = "budget-plans";
const key = "budget-plans";

export const useBudgetPlanApi = () => {
    const appContext = useAppContext();
    const queryClient = useQueryClient();

    const getBudgetPlanDetails = (budgetPlanId: string) => {
        return useQuery({
            queryKey: [key, "details", budgetPlanId],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.get<IBudgetPlanDetails>(`${budgetPlanUrlSegment}/${budgetPlanId}`);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<IBudgetPlanDetails>) => data.data as IBudgetPlanDetails,
            enabled: true,
        });
    };

    const browseBudgetPlans = (userId: string, pagination: IPaginationRequest) => {
        return useQuery({
            queryKey: [key, "browse", pagination.pageIndex, pagination.pageSize, pagination.isAscending],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.browse<IBudgetPlan>(`${budgetPlanUrlSegment}/browse/${userId}`, pagination);
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
                return await client.post(`${budgetPlanUrlSegment}/create`, budgetPlan);
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

    const removeBudgetPlan = useMutation({
        mutationKey: [key, "remove"],
        mutationFn: async (budgetPlanId: string) => {
            try {
                appContext.setLoading(true);
                return await client.delete(`${budgetPlanUrlSegment}/${budgetPlanId}/delete`);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries([key, "browse"]);
            await queryClient.invalidateQueries([key, "details"]);
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    const addExpense = useMutation({
        mutationKey: [key, "expense", "add"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            expense: { name: string; date: Date; amount: number; expenseCategory: string; }
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/expense/add`, payload.expense);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries([key, "browse"]);
            await queryClient.invalidateQueries([key, "details"]);
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    const removeExpense = useMutation({
        mutationKey: [key, "expense", "remove"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            expenseId: string;
        }) => {
            try {
                appContext.setLoading(true);
                return await client.delete(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/expense/${payload.expenseId}/delete`);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries([key, "browse"]);
            await queryClient.invalidateQueries([key, "details"]);
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    const addIncome = useMutation({
        mutationKey: [key, "income", "add"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            income: { name: string; date: Date; amount: number; incomeType: string; }
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/income/add`, payload.income);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries([key, "browse"]);
            await queryClient.invalidateQueries([key, "details"]);
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    const removeIncome = useMutation({
        mutationKey: [key, "income", "remove"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            incomeId: string;
        }) => {
            try {
                appContext.setLoading(true);
                return await client.delete(`${budgetPlanUrlSegment}/${payload.budgetPlanId}/income/${payload.incomeId}/delete`);
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries([key, "browse"]);
            await queryClient.invalidateQueries([key, "details"]);
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    return {
        queries: {
            getBudgetPlanDetails,
            browseBudgetPlans
        },
        commands: {
            createBudgetPlan,
            removeBudgetPlan,
            addExpense,
            removeExpense,
            addIncome,
            removeIncome
        }
    };
};
