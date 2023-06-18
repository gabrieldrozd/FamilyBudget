import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Badge, Box, Button, Card, Center, Container, Divider, Flex, Grid, Group, Paper, Stack, Text, Title} from "@mantine/core";
import {LocalLoader} from "@shared/components/LocalLoader";
import {useNavigate, useParams} from "react-router-dom";
import {IconX} from "@tabler/icons-react";

export const BudgetPlanDetailsPage = () => {
    const navigate = useNavigate();

    const {budgetPlanId} = useParams<{ budgetPlanId: string }>();

    const budgetPlanApi = useBudgetPlanApi();
    const {isLoading, data: budgetPlan} = budgetPlanApi.queries.getBudgetPlanDetails(budgetPlanId as string);
    const removeExpense = budgetPlanApi.commands.removeExpense;
    const removeIncome = budgetPlanApi.commands.removeIncome;

    const getFormattedDate = (date: string) => {
        const dateObj = new Date(date);
        return `${dateObj.getMonth() + 1}/${dateObj.getDate()}/${dateObj.getFullYear()}`;
    };

    return (
        <Container w="100%" size="xl">
            {isLoading ? (
                <LocalLoader loaderSize={100} />
            ) : (
                budgetPlan && (
                    <Card
                        p="md"
                        shadow="sm"
                        radius="lg"
                        style={{
                            height: "auto",
                            display: "flex",
                            flexDirection: "column",
                            position: "relative",
                        }}
                    >
                        <Flex mx={50} direction="column" align="center">
                            <Group w="100%" position="apart" style={{marginBottom: "8px"}}>
                                <Title order={2}>{budgetPlan.name}</Title>
                                <Badge p="md" fz="xl" color={budgetPlan.balance >= 0 ? "green" : "red"}>
                                    ${budgetPlan.balance}
                                </Badge>
                            </Group>

                            <Stack w="100%" style={{marginBottom: "8px"}}>
                                <Box>
                                    <Text fw={700}>Created by:</Text>
                                    {budgetPlan.creator.firstName} {budgetPlan.creator.lastName} ({budgetPlan.creator.email})
                                </Box>
                                <Box>
                                    <Text fw={700}>Start date:</Text>
                                    {getFormattedDate(budgetPlan.startDate)}
                                </Box>
                                <Box>
                                    <Text fw={700}>End date:</Text>
                                    {getFormattedDate(budgetPlan.endDate)}
                                </Box>
                                <Box>
                                    <Text fw={700}>Description:</Text>
                                    {budgetPlan.description}
                                </Box>
                            </Stack>

                            <div
                                style={{
                                    margin: "20px",
                                    width: "100%",
                                    height: "2px",
                                    backgroundColor: "#e0e0e0",
                                    marginBottom: "8px",
                                }}
                            />

                            <Grid w="100%">
                                <Grid.Col span={6}>
                                    <Title order={3}>Incomes</Title>
                                    <Flex direction="column" gap={20} w="100%">
                                        {budgetPlan.incomes.length === 0 && (
                                            <Paper p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="green.3">
                                                No incomes
                                            </Paper>
                                        )}
                                        {budgetPlan.incomes.map((income, index) => (
                                            <Flex
                                                key={index}
                                                p="sm"
                                                bg="green.3"
                                                direction="row"
                                                justify="space-between"
                                                style={{
                                                    marginBottom: "8px",
                                                    borderRadius: "8px"
                                                }}
                                            >
                                                {income.name}: ${income.amount}
                                                <IconX
                                                    onClick={() => removeIncome.mutate({
                                                        budgetPlanId: budgetPlan.externalId,
                                                        incomeId: income.externalId
                                                    })}
                                                    style={{cursor: "pointer"}}
                                                />
                                            </Flex>
                                        ))}
                                    </Flex>
                                </Grid.Col>

                                <Grid.Col span={6}>
                                    <Title order={3}>Expenses</Title>
                                    <Flex direction="column" gap={20} w="100%">
                                        {budgetPlan.expenses.length === 0 && (
                                            <Paper p="sm" shadow="xs" style={{marginBottom: "8px"}} bg="amber.3">
                                                No expenses
                                            </Paper>
                                        )}
                                        {budgetPlan.expenses.map((expense, index) => (
                                            <Flex
                                                key={index}
                                                p="sm"
                                                bg="amber.3"
                                                direction="row"
                                                justify="space-between"
                                                style={{
                                                    marginBottom: "8px",
                                                    borderRadius: "8px"
                                                }}
                                            >
                                                {expense.name}: ${expense.amount}
                                                <IconX
                                                    onClick={() => removeExpense.mutate({
                                                        budgetPlanId: budgetPlan.externalId,
                                                        expenseId: expense.externalId
                                                    })}
                                                    style={{cursor: "pointer"}}
                                                />
                                            </Flex>
                                        ))}
                                    </Flex>
                                </Grid.Col>
                            </Grid>
                        </Flex>

                        <Center>
                            <Button
                                mt={20}
                                mx={50}
                                fullWidth
                                size="lg"
                                radius="lg"
                                color="indigo.5"
                                variant="filled"
                                onClick={() => navigate("/")}
                            >
                                Go to your budget plans
                            </Button>
                        </Center>
                    </Card>
                )
            )}
        </Container>
    );
};