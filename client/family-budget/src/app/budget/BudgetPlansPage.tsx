import {BudgetPlanActions} from "@app/budget/components/BudgetPlanActions";
import {BudgetPlanExpenses} from "@app/budget/components/BudgetPlanExpenses";
import {BudgetPlanIncomes} from "@app/budget/components/BudgetPlanIncomes";
import {CreateBudgetPlanModalContent} from "@app/budget/components/ModalContent/CreateBudgetPlanModalContent";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {usePagination} from "@core/context/PaginationContextProvider";
import {Card, Container, Grid, Title, Text, Flex, Modal, Button, ScrollArea, Group, Badge} from "@mantine/core";
import {useDisclosure} from "@mantine/hooks";
import {LocalLoader} from "@shared/components/LocalLoader";
import {Paginator} from "@shared/components/Paginator";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {IconPlus} from "@tabler/icons-react";
import {motion} from "framer-motion";

const cardVariants = {
    hover: {
        y: -5,
        boxShadow: "0 8px 16px rgba(0, 0, 0, 0.1)",
    },
    initial: {
        y: 0,
        boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
    },
};

export const BudgetPlansPage = () => {
    const [opened, {open, close}] = useDisclosure(false);

    const {selectors: {accessToken}} = useAuthState();

    const pagination = usePagination();
    const budgetPlanApi = useBudgetPlanApi();
    const {isLoading, data, refetch} = budgetPlanApi.queries.browseBudgetPlans(accessToken().userId, pagination.model);

    return (
        <Container w="100%" size="xl">
            <Modal opened={opened} onClose={close} title="Create Budget Plan" centered xOffset={0}>
                <CreateBudgetPlanModalContent closeModal={close} />
            </Modal>

            <Flex mb={20} justify="space-between">
                <Title order={2}>Your Budget Plans</Title>
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
                                <Grid.Col key={plan.externalId} md={6} xs={12}>
                                    <motion.div
                                        initial="initial"
                                        whileHover="hover"
                                        variants={cardVariants}
                                        transition={{duration: 0.2}}
                                    >
                                        <Card
                                            p="md"
                                            shadow="sm"
                                            radius="lg"
                                            style={{
                                                height: "400px",
                                                display: "flex",
                                                flexDirection: "column",
                                                position: "relative",
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
                                                        <BudgetPlanIncomes plan={plan} />
                                                    </Grid.Col>

                                                    <Grid.Col span={6}>
                                                        <BudgetPlanExpenses plan={plan} />
                                                    </Grid.Col>
                                                </Grid>
                                            </ScrollArea>

                                            <Group position="apart">
                                                <BudgetPlanActions budgetPlanId={plan.externalId} />
                                            </Group>
                                        </Card>
                                    </motion.div>
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