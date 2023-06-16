import {Notify} from "@core/services/Notify";
import {useAuthState} from "@store/slices/auth/useAuthState";
import type {ReactNode} from "react";
import {useEffect} from "react";
import {useNavigate} from "react-router-dom";

export interface AuthorizedRouteProps {
    children: Element | ReactNode;
}

export const AuthorizedRoute = ({children}: AuthorizedRouteProps) => {
    const {selectors: {accessToken}} = useAuthState();
    const authResult = accessToken();
    const navigate = useNavigate();

    useEffect(() => {
        if (!authResult.userId) {
            Notify.error("Error", "You are not authorized!");
            navigate("/login");
        }
    }, []);

    return (
        <>
            {children}
        </>
    );
};