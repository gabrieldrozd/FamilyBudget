import type {IAccessToken} from "@core/models/auth";
import type {PayloadAction} from "@reduxjs/toolkit";
import {createSlice} from "@reduxjs/toolkit";

const SLICE_NAME = "auth";

export interface AuthSliceState {
    accessToken: IAccessToken;
    token: string | null;
}

const initialState: AuthSliceState = {
    accessToken: JSON.parse(localStorage.getItem("accessToken") || "{}") as IAccessToken,
    token: localStorage.getItem("token") || null
};

export const authSlice = createSlice({
    name: SLICE_NAME,
    initialState,
    reducers: {
        refresh: (state, action: PayloadAction<IAccessToken>) => {
            state.accessToken = action.payload;
            state.token = action.payload.token;
            localStorage.setItem("accessToken", JSON.stringify(action.payload));
            localStorage.setItem("token", action.payload.token);
        },
        login: (state, action: PayloadAction<IAccessToken>) => {
            state.accessToken = action.payload;
            state.token = action.payload.token;
            localStorage.setItem("accessToken", JSON.stringify(action.payload));
            localStorage.setItem("token", action.payload.token);
        },
        logout: (state) => {
            state.accessToken = {} as IAccessToken;
            state.token = null;
            localStorage.removeItem("accessToken");
            localStorage.removeItem("token");
        }
    }
});
