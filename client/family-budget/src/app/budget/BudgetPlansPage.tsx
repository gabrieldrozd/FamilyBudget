import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {usePagination} from "@core/context/PaginationContextProvider";
import {Card, Container, Grid, Title, Text, Flex, Modal, Button} from "@mantine/core";
import {useDisclosure} from "@mantine/hooks";
import {LocalLoader} from "@shared/components/LocalLoader";
import {Paginator} from "@shared/components/Paginator";
import {RegisterUserModalContent} from "@app/budget/components/RegisterUserModalContent";
import {IconPlus} from "@tabler/icons-react";
import {CreateBudgetPlanModalContent} from "@app/budget/components/CreateBudgetPlanModalContent";

export const BudgetPlansPage = () => {
    const [opened, {open, close}] = useDisclosure(false);

    const pagination = usePagination();
    const userApi = useBudgetPlanApi();
    const {isLoading, data, refetch} = userApi.queries.browseBudgetPlans(pagination.model);

    return (
        <Container w="100%">
            <Modal opened={opened} onClose={close} title="Add new user" centered>
                <CreateBudgetPlanModalContent closeModal={close} />
            </Modal>

            <Flex mb={20} justify="flex-end">
                <Button color="indigo.5" variant="filled" onClick={open} rightIcon={<IconPlus />}>
                    Add Budget Plan
                </Button>
            </Flex>

            <Flex
                direction="column"
                justify="center"
                align="center"
                w="100%"
            >
                {isLoading ? (<LocalLoader loaderSize={100} />) : (
                    <>
                        <Grid gutter="lg" columns={3} style={{width: "100%"}}>
                            {data?.list?.map((plan) => (
                                <Card
                                    key={plan.externalId}
                                    padding="md"
                                    style={{marginBottom: "16px"}}
                                >
                                    <Title order={6}>{plan.name}</Title>
                                    <Text>{plan.description}</Text>
                                    <Text>Balance: ${plan.balance}</Text>
                                    <Text>Start Date: {new Date(plan.startDate).toLocaleDateString()}</Text>
                                    <Text>End Date: {new Date(plan.endDate).toLocaleDateString()}</Text>
                                </Card>
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