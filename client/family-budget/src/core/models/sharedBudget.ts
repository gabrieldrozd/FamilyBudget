import type {IExpense} from "@core/models/expense";
import type {IIncome} from "@core/models/income";

export interface ISharedBudget {
    externalId: string;
    budgetExternalId: string;
    dateShared: Date;
    name: string;
    description: string;
    balance: number;
    startDate: Date;
    endDate: Date;
    incomes: IIncome[];
    expenses: IExpense[];
}