export interface IExpense {
    externalId: string;
    name: string;
    date: Date;
    amount: number;
    expenseCategory: string;
}