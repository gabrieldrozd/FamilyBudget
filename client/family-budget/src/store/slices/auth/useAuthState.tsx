import {useAppContext} from "@core/context/ApplicationContext";
import type {IAccessToken, Role} from "@core/models/auth/IAccessToken";
import {Notify} from "@core/services/Notify";
import {authSlice} from "@store/slices/auth/authSlice";
import type {ActionDispatch, RootState} from "@store/store";
import {useDispatch, useSelector} from "react-redux";
import {useNavigate} from "react-router-dom";

export const useAuthState = () => {
        const state = useSelector((state: RootState) => state.auth);
        const dispatch = useDispatch<ActionDispatch>();
        const actions = authSlice.actions;
        const {setLoading} = useAppContext();
        const navigate = useNavigate();

        const authActions = {
            refresh: (accessToken: IAccessToken) => {
                dispatch(actions.refresh(accessToken));
                return true;
            },
            login: (accessToken: IAccessToken) => {
                dispatch(actions.login(accessToken));
                Notify.success("Hey, Hi!", `Welcome back ${accessToken.firstName}!`);
                navigate("/");
            },
            logout: () => {
                setLoading(true);
                dispatch(actions.logout());
                setLoading(false);
                navigate("/login");
            }
        };

        const authSelectors = {
            token: () => state.token,
            isInRole: (givenRole: Role) => state.accessToken.role === givenRole,
            isAuthenticated: () => state.accessToken !== null,
            accessToken: () => state.accessToken
        };

        return {
            actions: authActions,
            selectors: authSelectors
        };
    }
;