import type {AnyAction, EnhancedStore, Middleware} from "@reduxjs/toolkit";
import {configureStore} from "@reduxjs/toolkit";
import {initializeAction} from "@store/persistActions";
import {loadStateFromIndexedDB, stateMiddleware} from "@store/persistMiddleware";
import {authSlice} from "@store/slices/auth/authSlice";

export interface RootState {
    auth: ReturnType<typeof authSlice.reducer>;
    // user: ReturnType<typeof userSlice.reducer>;
}

export const defaultState: RootState = {
    auth: authSlice.reducer(undefined, {type: ""}),
    // user: userSlice.reducer(undefined, {type: ""}),
};

const preloadedState: RootState | undefined = await loadStateFromIndexedDB().then((state) => {
    if (state) {
        return state;
    } else {
        return undefined;
    }
});

export const store: EnhancedStore<RootState, AnyAction, Middleware[]> = configureStore({
    reducer: {
        auth: authSlice.reducer,
        // user: userSlice.reducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({}).concat(stateMiddleware),
    preloadedState
});

export declare type ActionDispatch = typeof store.dispatch;

store.dispatch(initializeAction);