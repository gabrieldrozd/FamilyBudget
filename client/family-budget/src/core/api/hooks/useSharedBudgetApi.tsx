import {AxiosClient} from "@core/api/AxiosClient";
import {useAppContext} from "@core/context/ApplicationContext";
import type {DataEnvelope} from "@core/models/dataEnvelope";
import type {IPaginatedList, IPaginationRequest} from "@core/models/pagination";
import type {ISharedBudget, ISharedBudgetDetails} from "@core/models/sharedBudget";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";

const client = AxiosClient.initialize();
const sharedBudgetUrlSegment = "shared-budgets";
const key = "shared-budgets";

export const useSharedBudgetApi = () => {
    const appContext = useAppContext();
    const queryClient = useQueryClient();

    const getSharedBudgetDetails = (sharedBudgetId: string) => {
        return useQuery({
            queryKey: [key, "details", sharedBudgetId],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.get<ISharedBudgetDetails>(`${sharedBudgetUrlSegment}/${sharedBudgetId}`);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<ISharedBudgetDetails>) => data.data as ISharedBudgetDetails,
            enabled: true,
        });
    };

    const browseSharedBudgets = (userId: string, pagination: IPaginationRequest) => {
        return useQuery({
            queryKey: [key, "browse", pagination.pageIndex, pagination.pageSize, pagination.isAscending],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.browse<ISharedBudget>(`${sharedBudgetUrlSegment}/browse/${userId}`, pagination);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<IPaginatedList<ISharedBudget>>) => data.data as IPaginatedList<ISharedBudget>,
            enabled: true,
        });
    };

    const shareBudgetPlan = useMutation({
        mutationKey: [key, "share"],
        mutationFn: async (payload: {
            budgetPlanId: string;
            userIds: string[];
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post(`${sharedBudgetUrlSegment}/${payload.budgetPlanId}/share`, {
                    budgetPlanId: payload.budgetPlanId,
                    userExternalIds: payload.userIds
                });
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
            await queryClient.invalidateQueries(["shared-budgets", "browse"]);
        }
    });

    return {
        queries: {
            getSharedBudgetDetails,
            browseSharedBudgets,
        },
        commands: {
            shareBudgetPlan,
        }
    };
};
