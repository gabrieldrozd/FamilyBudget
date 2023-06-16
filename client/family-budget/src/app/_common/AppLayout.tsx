import {AppNavbar} from "@app/_common/AppNavbar";
import {Container, Flex} from "@mantine/core";
import {Outlet} from "react-router-dom";

export const AppLayout = () => {
    return (
        <Flex
            direction="column"
            w="100%"
            h="100%"
        >
            <AppNavbar />
            <Container size="xl" w="100%" h="100%">
                <Outlet />
            </Container>
        </Flex>
    );
};

