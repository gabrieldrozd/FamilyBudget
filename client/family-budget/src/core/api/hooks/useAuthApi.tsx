import {AxiosClient} from "@core/api/AxiosClient";
import {useAppContext} from "@core/context/ApplicationContext";
import type {DataEnvelope} from "@core/models/dataEnvelope";
import type {IAccessToken} from "@core/models/auth";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";

const client = AxiosClient.initialize();
const authUrlSegment = "auth";
const key = "auth";

export const useAuthApi = () => {
    const appContext = useAppContext();
    const queryClient = useQueryClient();
    const authState = useAuthState();

    const refreshToken = () => {
        return useQuery({
            queryKey: [key, "refreshToken"],
            queryFn: async () => {
                appContext.setLoading(true);
                const accessToken = await client.get<IAccessToken>(authUrlSegment);
                authState.actions.refresh(accessToken.data);
                appContext.setLoading(false);
                return accessToken;
            },
            select: (data: DataEnvelope<IAccessToken>) => data.data as IAccessToken,
            enabled: false,
        });
    };

    const login = useMutation({
        mutationKey: [key, "login"],
        mutationFn: async (credentials: { email: string, password: string }) => {
            try {
                appContext.setLoading(true);
                const accessToken = await client.post<IAccessToken>(`${authUrlSegment}/login`, {
                    email: credentials.email,
                    password: credentials.password,
                });
                authState.actions.login(accessToken.data);
                return accessToken;
            } catch (ex) {
                console.log(ex);
                appContext.setLoading(false);
                throw ex;
            } finally {
                appContext.setLoading(false);
            }
        },
        onSuccess: async () => await queryClient.invalidateQueries([key, "refreshToken"])
    });

    return {
        queries: {
            refreshToken,
        },
        commands: {
            login,
        }
    };
};
