import {AddExpensePopoverContent} from "@app/budget/components/AddExpensePopoverContent";
import {AddIncomePopoverContent} from "@app/budget/components/AddIncomePopoverContent";
import {Button, Group, Popover} from "@mantine/core";
import {colors} from "@shared/colors";

interface Props {
    budgetPlanId: string;
}

export const BudgetPlanActions = ({budgetPlanId}: Props) => {
    return (
        <>
            <Button color="red.6" size="sm" onClick={() => console.log()} style={{borderRadius: "15px"}}>
                Remove
            </Button>
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
                                border: `2px solid ${colors.green500}`
                            }
                        }}
                    >
                        <Popover.Target>
                            <Button
                                color="green.6"
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
                                border: `2px solid ${colors.amber500}`
                            }
                        }}
                    >
                        <Popover.Target>
                            <Button
                                color="amber.6"
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
        </>
    );
};