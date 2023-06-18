import type {IBudgetPlan} from "@core/models/budgetPlan";
import {Center, Flex, Group, Paper} from "@mantine/core";
import {IconDots, IconX} from "@tabler/icons-react";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";

interface Props {
    plan: IBudgetPlan;
}

export const BudgetPlanExpenses = ({plan}: Props) => {
    const budgetPlanApi = useBudgetPlanApi();
    const removeExpense = budgetPlanApi.commands.removeExpense;

    const getShortenedName = (name: string) => {
        if (name.length > 15) {
            return name.slice(0, 15) + "...";
        }
        return name;
    };

    return (
        <>
            {plan.expenses.length === 0 && (
                <Paper mt={20} p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="amber.3">
                    No expenses
                </Paper>
            )}
            {plan.expenses.slice(0, 3).map((expense, index) => (
                <Flex
                    key={index}
                    mt={20}
                    p="sm"
                    bg="amber.3"
                    direction="row"
                    justify="space-between"
                    style={{
                        marginBottom: "8px",
                        borderRadius: "8px"
                    }}
                >
                    {getShortenedName(expense.name)}: ${expense.amount}
                    <IconX
                        onClick={() => removeExpense.mutate({
                            budgetPlanId: plan.externalId,
                            expenseId: expense.externalId
                        })}
                        style={{cursor: "pointer"}}
                    />
                </Flex>
            ))}
            <Center>
                {plan.expenses.length > 3 && (<IconDots size={30} />)}
            </Center>
        </>
    );
};