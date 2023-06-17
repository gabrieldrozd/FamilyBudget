import {CreateBudgetPlanModalContent} from "@app/budget/components/CreateBudgetPlanModalContent";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {usePagination} from "@core/context/PaginationContextProvider";
import {
    Card,
    Container,
    Grid,
    Title,
    Text,
    Flex,
    Modal,
    Button,
    ScrollArea,
    Paper,
    Group,
    Badge,
    Popover,
    UnstyledButton
} from "@mantine/core";
import {useDisclosure} from "@mantine/hooks";
import {LocalLoader} from "@shared/components/LocalLoader";
import {Paginator} from "@shared/components/Paginator";
import {IconPlus} from "@tabler/icons-react";
import {AddIncomePopoverContent} from "@app/budget/components/AddIncomePopoverContent";
import {AddExpensePopoverContent} from "@app/budget/components/AddExpensePopoverContent";
import {colors} from "@shared/colors";

export const BudgetPlansPage = () => {
    const [opened, {open, close}] = useDisclosure(false);

    const pagination = usePagination();
    const userApi = useBudgetPlanApi();
    const {isLoading, data, refetch} = userApi.queries.browseBudgetPlans(pagination.model);

    return (
        <Container w="100%" size="lg">
            <Modal opened={opened} onClose={close} title="Add new user" centered>
                <CreateBudgetPlanModalContent closeModal={close} />
            </Modal>

            <Flex mb={20} justify="space-between">
                <Title order={2}>Budget Plans</Title>
                <Button color="indigo.5" variant="filled" onClick={open} rightIcon={<IconPlus />}>
                    Add Budget Plan
                </Button>
            </Flex>

            <Flex
                mb={20}
                direction="column"
                justify="center"
                align="center"
            >
                {isLoading ? (<LocalLoader loaderSize={100} />) : (
                    <>
                        <Grid gutter="xl" w="100%">
                            {data?.list?.map((plan) => (
                                <Grid.Col key={plan.externalId} sm={6} xs={12}>
                                    <Card
                                        p="md"
                                        shadow="sm"
                                        style={{
                                            height: "350px",
                                            display: "flex",
                                            flexDirection: "column",
                                            position: "relative"
                                        }}
                                    >
                                        <Flex justify="space-between" style={{marginBottom: "8px"}}>
                                            <Title order={4}>{plan.name}</Title>
                                            <Badge p="md" fz="xl" color={plan.balance >= 0 ? "green" : "red"}>
                                                ${plan.balance}
                                            </Badge>
                                        </Flex>
                                        <Text>{plan.description}</Text>

                                        <ScrollArea
                                            style={{flexGrow: 1, overflowY: "auto", marginBottom: "8px"}}
                                            styles={{scrollbar: {display: "hidden"}}}
                                        >
                                            <Grid gutter={20}>
                                                <Grid.Col span={6}>
                                                    {plan.incomes.slice(0, 3).map((income, index) => (
                                                        <Paper key={index} p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="green.3">
                                                            {income.name}: ${income.amount}
                                                        </Paper>
                                                    ))}
                                                    {plan.incomes.length > 3 && <Text>...</Text>}
                                                </Grid.Col>

                                                <Grid.Col span={6}>
                                                    {plan.expenses.slice(0, 3).map((expense, index) => (
                                                        <Paper key={index} p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="amber.3">
                                                            {expense.name}: ${expense.amount}
                                                        </Paper>
                                                    ))}
                                                    {plan.expenses.length > 3 && <Text>...</Text>}
                                                </Grid.Col>
                                            </Grid>
                                        </ScrollArea>

                                        <Group position="apart">
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
                                                            <AddIncomePopoverContent budgetPlanId={plan.externalId} />
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
                                                            <AddExpensePopoverContent budgetPlanId={plan.externalId} />
                                                        </Popover.Dropdown>
                                                    </Popover>
                                                </Button.Group>
                                            </Group>
                                        </Group>
                                    </Card>
                                </Grid.Col>
                            ))}
                        </Grid>

                        <Paginator
                            data={data!}
                            pagination={pagination}
                            refetch={refetch}
                        />
                    </>
                )}
            </Flex>
        </Container>
    );
};