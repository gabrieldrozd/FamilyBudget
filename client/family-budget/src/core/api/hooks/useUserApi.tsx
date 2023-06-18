import {AxiosClient} from "@core/api/AxiosClient";
import {useAppContext} from "@core/context/ApplicationContext";
import type {DataEnvelope} from "@core/models/dataEnvelope";
import type {IPaginatedList, IPaginationRequest} from "@core/models/pagination";
import type {IUserBase} from "@core/models/user";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";

const client = AxiosClient.initialize();
const usersUrlSegment = "users";
const key = "users";

export const useUserApi = () => {
    const appContext = useAppContext();
    const queryClient = useQueryClient();

    const allUsers = () => {
        return useQuery({
            queryKey: [key, "all"],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.get<IUserBase[]>(`${usersUrlSegment}/all`);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<IUserBase[]>) => data.data as IUserBase[],
            enabled: true,
        });
    };

    const browseUsers = (pagination: IPaginationRequest) => {
        return useQuery({
            queryKey: [key, "browse", pagination.pageIndex, pagination.pageSize, pagination.isAscending],
            queryFn: () => {
                appContext.setLoading(true);
                const result = client.browse<IUserBase>(`${usersUrlSegment}/browse`, pagination);
                appContext.setLoading(false);
                return result;
            },
            select: (data: DataEnvelope<IPaginatedList<IUserBase>>) => data.data as IPaginatedList<IUserBase>,
            enabled: true,
        });
    };

    const registerUser = useMutation({
        mutationKey: [key, "register"],
        mutationFn: async (user: {
            email: string;
            firstName: string;
            lastName: string;
            role: string;
        }) => {
            try {
                appContext.setLoading(true);
                return await client.post<IUserBase>(`${usersUrlSegment}/register`, user);
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

    const deleteUser = useMutation({
        mutationKey: [key, "delete"],
        mutationFn: async (id: string) => {
            try {
                appContext.setLoading(true);
                return await client.delete(`${usersUrlSegment}/${id}/delete`);
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
            allUsers,
            browseUsers
        },
        commands: {
            registerUser,
            deleteUser
        }
    };
};
