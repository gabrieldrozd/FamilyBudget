import {authSlice} from "@store/slices/auth/authSlice";

export const initializeAction = {type: "initialize"};

export const persistActions = [
    initializeAction,
    authSlice.actions.refresh,
    authSlice.actions.login,
    authSlice.actions.logout
];

