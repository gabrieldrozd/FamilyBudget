import {CreateBudgetPlanModalContent} from "@app/budget/components/CreateBudgetPlanModalContent";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {usePagination} from "@core/context/PaginationContextProvider";
import {Card, Container, Grid, Title, Text, Flex, Modal, Button, ScrollArea, Paper, Group, Badge} from "@mantine/core";
import {useDisclosure} from "@mantine/hooks";
import {LocalLoader} from "@shared/components/LocalLoader";
import {Paginator} from "@shared/components/Paginator";
import {IconPlus} from "@tabler/icons-react";

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
                        <Grid gutter="xl">
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

                                        <ScrollArea style={{flexGrow: 1, overflowY: "auto", marginBottom: "8px"}}>
                                            {plan.incomes.slice(0, 3).map((income, index) => (
                                                <Paper key={index} p="sm" shadow="xs" style={{marginBottom: "8px"}}>
                                                    {income.name}: ${income.amount}
                                                </Paper>
                                            ))}
                                            {plan.incomes.length > 3 && <Text>...</Text>}

                                            {plan.expenses.slice(0, 3).map((expense, index) => (
                                                <Paper key={index} p="sm" shadow="xs" style={{marginBottom: "8px"}}>
                                                    {expense.name}: ${expense.amount}
                                                </Paper>
                                            ))}
                                            {plan.expenses.length > 3 && <Text>...</Text>}
                                        </ScrollArea>

                                        <Group position="apart">
                                            <Button color="red.6" size="sm" onClick={() => console.log()} style={{borderRadius: "15px"}}>
                                                Remove
                                            </Button>
                                            <Group>
                                                <Button.Group>
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