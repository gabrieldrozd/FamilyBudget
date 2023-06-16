import type {IExpense} from "@core/models/expense";
import type {IIncome} from "@core/models/income";

export interface IBudgetPlan {
    externalId: string;
    name: string;
    description: string;
    balance: number;
    startDate: Date;
    endDate: Date;
    incomes: IIncome[];
    expenses: IExpense[];
}