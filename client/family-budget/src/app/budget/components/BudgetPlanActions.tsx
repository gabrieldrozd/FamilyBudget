import {AddExpensePopoverContent} from "@app/budget/components/PopoverContent/AddExpensePopoverContent";
import {AddIncomePopoverContent} from "@app/budget/components/PopoverContent/AddIncomePopoverContent";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {Button, Flex, Group, Popover, Text} from "@mantine/core";
import {colors} from "@shared/colors";
import {useSharedBudgetApi} from "@core/api/hooks/useSharedBudgetApi";
import {useUserApi} from "@core/api/hooks/useUserApi";
import {LocalLoader} from "@shared/components/LocalLoader";
import {SharedBudgetsPage} from "@app/budget/SharedBudgetsPage";
import {ShareBudgetPlanPopoverContent} from "@app/budget/components/PopoverContent/ShareBudgetPlanPopoverContent";
import {useNavigate} from "react-router-dom";

interface Props {
    budgetPlanId: string;
}

export const BudgetPlanActions = ({budgetPlanId}: Props) => {
    const navigate = useNavigate();

    const budgetPlanApi = useBudgetPlanApi();
    const removeBudgetPlan = budgetPlanApi.commands.removeBudgetPlan;

    const sharedBudgetApi = useSharedBudgetApi();
    const shareBudgetPlan = sharedBudgetApi.commands.shareBudgetPlan;

    const userApi = useUserApi();
    const {isLoading, data: availableUsers} = userApi.queries.allUsers();

    const handleRemoveBudgetPlan = () => {
        removeBudgetPlan.mutate(budgetPlanId, {
            onSuccess: () => Notify.success("Budget plan removed!")
        });
    };

    const handleShareBudgetPlan = (userIds: string[]) => {
        shareBudgetPlan.mutate({budgetPlanId: budgetPlanId, userIds: userIds}, {
            onSuccess: () => Notify.success("Budget plan shared with selected users!")
        });
    };

    if (isLoading) {
        return <LocalLoader loaderSize={100} />;
    }

    return (
        <>
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
                                border: `2px solid ${colors.blue400}`
                            }
                        }}
                    >
                        <Popover.Target>
                            <Button
                                w={100}
                                color="blue.5"
                                size="sm"
                                onClick={() => console.log()}
                                style={{
                                    borderTopLeftRadius: "15px",
                                    borderBottomLeftRadius: "15px",
                                }}
                            >
                                Share
                            </Button>
                        </Popover.Target>
                        <Popover.Dropdown miw={500}>
                            {availableUsers &&
                                <ShareBudgetPlanPopoverContent
                                    availableUsers={availableUsers}
                                    shareBudgetPlan={handleShareBudgetPlan}
                                />
                            }
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
                                border: `2px solid ${colors.red400}`
                            }
                        }}
                    >
                        <Popover.Target>
                            <Button
                                w={100}
                                color="red.5"
                                size="sm"
                                style={{
                                    borderTopRightRadius: "15px",
                                    borderBottomRightRadius: "15px",
                                }}
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
                </Button.Group>
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