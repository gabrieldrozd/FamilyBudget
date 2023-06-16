import {AppLayout} from "@app/_common/AppLayout";
import {NotFoundPage} from "@app/_common/NotFoundPage";
import {StartPage} from "@app/_start/StartPage";
import {BudgetPlansPage} from "@app/budget/BudgetPlansPage";
import {SharedBudgetsPage} from "@app/budget/SharedBudgetsPage";
import {UsersPage} from "@app/budget/UsersPage";
import {PaginationContextProvider} from "@core/context/PaginationContextProvider";
import {AuthorizedRoute} from "@core/routing/components/AuthorizedRoute";
import {createBrowserRouter, createRoutesFromElements, Route} from "react-router-dom";

export const ApplicationRouter = createBrowserRouter(
    createRoutesFromElements(
        <Route>
            <Route path="login" element={<StartPage />} />
            <Route
                path="/"
                element={
                    <AuthorizedRoute>
                        <AppLayout />
                    </AuthorizedRoute>
                }
            >
                <Route
                    index
                    element={
                        <PaginationContextProvider>
                            <BudgetPlansPage />
                        </PaginationContextProvider>
                    }
                />
                <Route path="shared" element={<SharedBudgetsPage />} />
                <Route
                    path="users"
                    element={
                        <PaginationContextProvider>
                            <UsersPage />
                        </PaginationContextProvider>
                    }
                />
            </Route>
            <Route path="*" element={<NotFoundPage />} />
        </Route>
    )
);