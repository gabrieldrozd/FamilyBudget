import type {IBudgetPlan} from "@core/models/budgetPlan";
import type {ISharedBudget} from "@core/models/sharedBudget";

export interface IUser {
    externalId: string;
    email: string;
    firstName: string;
    lastName: string;
    role: string;
    budgetPlans: IBudgetPlan[];
    sharedBudgets: ISharedBudget[];
}

export interface IUserBase {
    externalId: string;
    email: string;
    firstName: string;
    lastName: string;
    role: string;
    budgetPlans: number;
    sharedBudgets: number;
}