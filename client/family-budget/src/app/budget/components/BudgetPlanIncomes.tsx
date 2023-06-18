import type {IBudgetPlan} from "@core/models/budgetPlan";
import {Center, Flex, Paper} from "@mantine/core";
import {IconDots, IconX} from "@tabler/icons-react";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";

interface Props {
    plan: IBudgetPlan;
}

export const BudgetPlanIncomes = ({plan}: Props) => {
    const budgetPlanApi = useBudgetPlanApi();
    const removeIncome = budgetPlanApi.commands.removeIncome;

    const getShortenedName = (name: string) => {
        if (name.length > 15) {
            return name.slice(0, 15) + "...";
        }
        return name;
    };

    return (
        <>
            {plan.incomes.length === 0 && (
                <Paper mt={20} p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="green.3">
                    No incomes
                </Paper>
            )}
            {plan.incomes.slice(0, 3).map((income, index) => (
                <Flex
                    key={index}
                    mt={20}
                    p="sm"
                    bg="green.3"
                    direction="row"
                    justify="space-between"
                    style={{
                        marginBottom: "8px",
                        borderRadius: "8px"
                    }}
                >
                    {getShortenedName(income.name)}: ${income.amount}
                    <IconX
                        onClick={() => removeIncome.mutate({
                            budgetPlanId: plan.externalId,
                            incomeId: income.externalId
                        })}
                        style={{cursor: "pointer"}}
                    />
                </Flex>
            ))}
            <Center>
                {plan.incomes.length > 3 && (<IconDots size={30} />)}
            </Center>
        </>
    );
};