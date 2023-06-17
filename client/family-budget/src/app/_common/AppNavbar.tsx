import {Container, Flex, Header, Paper, Space, Text, UnstyledButton} from "@mantine/core";
import {colors} from "@shared/colors";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {IconX} from "@tabler/icons-react";
import {Link, useLocation} from "react-router-dom";

const navItems = [
    {title: "Budget", path: "/", role: ""},
    {title: "Shared", path: "/shared", role: ""},
    {title: "Users", path: "/users", role: "Owner"},
];

export const AppNavbar = () => {
    const {actions: {logout}, selectors: {isInRole}} = useAuthState();
    const location = useLocation();

    return (
        <Header height={80} mb={20}>
            <Container size="lg">
                <Flex
                    direction="row"
                    justify="space-between"
                    align="center"
                    h="100%"
                >
                    <Flex h="100%" direction="column" justify="center" align="center">
                        <Text size="xl" weight={700} color="indigo.7">Family Budget</Text>
                        <Text size="xl" weight={700}>Manager</Text>
                    </Flex>
                    <Flex h="100%">
                        {navItems.map((item) => {
                            if (!item.role || isInRole(item.role)) {
                                return (
                                    <Paper key={item.path} py="sm" px="xs" my="xs" style={{borderRadius: 4}}>
                                        <Link
                                            to={item.path}
                                            style={{
                                                display: "inline-block",
                                                textDecoration: "none",
                                                padding: "8px",
                                                borderRadius: 4,
                                                color: "black",
                                                backgroundColor: location.pathname === item.path ? colors.indigo200 : "transparent",
                                                fontWeight: location.pathname === item.path ? 700 : 400,
                                            }}
                                        >
                                            {item.title}
                                        </Link>
                                    </Paper>
                                );
                            }
                            return null;
                        })}
                    </Flex>
                    <Flex h="100%">
                        <Paper py="sm" my="xs" style={{borderRadius: 4}}>
                            <UnstyledButton
                                onClick={logout}
                                style={{
                                    fontWeight: 700,
                                    display: "inline-block",
                                    textDecoration: "none",
                                    padding: "8px",
                                }}
                            >
                                <Flex>
                                    <Text>Logout</Text>
                                    <Space w={5} />
                                    <IconX size={22} />
                                </Flex>
                            </UnstyledButton>
                        </Paper>
                    </Flex>
                </Flex>
            </Container>
        </Header>
    );
};
