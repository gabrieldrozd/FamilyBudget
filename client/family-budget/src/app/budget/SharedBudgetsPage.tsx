import {Container, Flex} from "@mantine/core";

export const SharedBudgetsPage = () => {
    return (
        <Container w="100%">
            <Flex
                direction="column"
                justify="center"
                align="center"
                style={{backgroundColor: "red"}}
                w="100%"
            >
                Shared Budgets Page
            </Flex>
        </Container>
    );
};