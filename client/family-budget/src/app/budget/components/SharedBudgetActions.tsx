import {AddExpensePopoverContent} from "@app/budget/components/PopoverContent/AddExpensePopoverContent";
import {AddIncomePopoverContent} from "@app/budget/components/PopoverContent/AddIncomePopoverContent";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {Button, Flex, Group, Popover, Text} from "@mantine/core";
import {colors} from "@shared/colors";
import {useNavigate} from "react-router-dom";

interface Props {
    budgetPlanId: string;
}

export const SharedBudgetActions = ({budgetPlanId}: Props) => {
    const navigate = useNavigate();

    return (
        <>
            <Flex w="100%" gap={20}>
                <Popover
                    width={200}
                    position="bottom"
                    withArrow
                    shadow="xl"
                    arrowSize={14}
                    styles={{
                        dropdown: {
                            borderRadius: "20px",
                            border: `2px solid ${colors.green600}`
                        }
                    }}
                >
                    <Popover.Target>
                        <Button
                            variant="light"
                            color="green.7"
                            size="sm"
                            radius="lg"
                            fullWidth
                            onClick={() => console.log()}
                        >
                            Add Income
                        </Button>
                    </Popover.Target>
                    <Popover.Dropdown miw={500}>
                        <AddIncomePopoverContent budgetPlanId={budgetPlanId} />
                    </Popover.Dropdown>
                </Popover>

                <Popover
                    width={200}
                    position="bottom"
                    withArrow
                    shadow="xl"
                    arrowSize={14}
                    styles={{
                        dropdown: {
                            borderRadius: "20px",
                            border: `2px solid ${colors.amber600}`
                        }
                    }}
                >
                    <Popover.Target>
                        <Button
                            variant="light"
                            color="amber.7"
                            size="sm"
                            radius="lg"
                            fullWidth
                            onClick={() => console.log()}
                        >
                            Add Expense
                        </Button>
                    </Popover.Target>
                    <Popover.Dropdown miw={500}>
                        <AddExpensePopoverContent budgetPlanId={budgetPlanId} />
                    </Popover.Dropdown>
                </Popover>
            </Flex>

            <Button
                size="md"
                radius="lg"
                fullWidth
                variant="light"
                color="indigo.7"
                onClick={() => navigate(`/${budgetPlanId}`)}
            >
                Go to details
            </Button>
        </>
    );
};