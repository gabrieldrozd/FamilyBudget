import type {IExpense} from "@core/models/expense";
import type {IIncome} from "@core/models/income";
import {IUserBase} from "@core/models/user";

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

export interface IBudgetPlanDetails {
  externalId: string
  name: string
  description: string
  balance: number
  startDate: string
  endDate: string
  incomes: IIncome[]
  expenses: IExpense[]
  creator: IUserBase
}