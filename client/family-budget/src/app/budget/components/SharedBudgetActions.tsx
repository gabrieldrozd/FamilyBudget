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

    const budgetPlanApi = useBudgetPlanApi();
    const removeBudgetPlan = budgetPlanApi.commands.removeBudgetPlan;

    const handleRemoveBudgetPlan = () => {
        removeBudgetPlan.mutate(budgetPlanId, {
            onSuccess: () => Notify.success("Budget plan removed!")
        });
    };

    return (
        <>
            <Group>
                <Popover
                    width={200}
                    position="bottom"
                    withArrow
                    shadow="xl"
                    arrowSize={14}
                    styles={{
                        dropdown: {
                            borderRadius: "20px",
                            border: `2px solid ${colors.red400}`
                        }
                    }}
                >
                    <Popover.Target>
                        <Button
                            w={100}
                            color="red.5"
                            size="sm"
                            style={{borderRadius: "15px"}}
                        >
                            Remove
                        </Button>
                    </Popover.Target>
                    <Popover.Dropdown miw={500}>
                        <Flex
                            direction="column"
                            justify="center"
                            align="center"
                        >
                            <Text>Are you sure you want to remove this budget plan?</Text>
                            <Button
                                mt={20}
                                color="red"
                                radius="lg"
                                onClick={handleRemoveBudgetPlan}
                            >
                                Yes, remove it
                            </Button>
                        </Flex>
                    </Popover.Dropdown>
                </Popover>
            </Group>

            <Group>
                <Button.Group>
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
                                onClick={() => console.log()}
                                style={{
                                    borderTopLeftRadius: "15px",
                                    borderBottomLeftRadius: "15px",
                                }}
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
                                onClick={() => console.log()}
                                style={{
                                    borderTopRightRadius: "15px",
                                    borderBottomRightRadius: "15px",
                                }}
                            >
                                Add Expense
                            </Button>
                        </Popover.Target>
                        <Popover.Dropdown miw={500}>
                            <AddExpensePopoverContent budgetPlanId={budgetPlanId} />
                        </Popover.Dropdown>
                    </Popover>
                </Button.Group>
            </Group>

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