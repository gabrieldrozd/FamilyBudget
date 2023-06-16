export interface IUser {
    externalId: string;
    email: string;
    firstName: string;
    lastName: string;
    role: string;
    // Todo: Add budgetPlans and sharedBudgets
    // budgetPlans: IBudgetPlan[];
    // sharedBudgets: ISharedBudget[];
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