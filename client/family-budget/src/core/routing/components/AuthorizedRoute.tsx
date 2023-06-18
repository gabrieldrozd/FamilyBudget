import {Notify} from "@core/services/Notify";
import {useAuthState} from "@store/slices/auth/useAuthState";
import type {ReactNode} from "react";
import {useEffect} from "react";
import {useNavigate} from "react-router-dom";
import {useAuthApi} from "@core/api/hooks/useAuthApi";
import {LocalLoader} from "@shared/components/LocalLoader";

export interface AuthorizedRouteProps {
    children: Element | ReactNode;
}

export const AuthorizedRoute = ({children}: AuthorizedRouteProps) => {
    const {selectors: {accessToken}} = useAuthState();
    const authResult = accessToken();
    const navigate = useNavigate();

    const authApi = useAuthApi();
    const {isLoading, data: isAuthorized} = authApi.queries.refreshToken();

    useEffect(() => {
        if (!authResult.userId) {
            Notify.error("Error", "You are not authorized!");
            navigate("/login");
        }
    }, []);

    if (isLoading) {
        return <LocalLoader loaderSize={100} />;
    }

    if (!isAuthorized) {
        Notify.error("Error", "You are not authorized!");
        navigate("/login");
        return null;
    }

    return (
        <>
            {children}
        </>
    );
};