import {Container, Flex, Header, Mark, Paper, Space, Text, UnstyledButton} from "@mantine/core";
import {colors} from "@shared/colors";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {IconX} from "@tabler/icons-react";
import {Link, useLocation, useNavigate} from "react-router-dom";

const navItems = [
    {title: "Budget", path: "/", role: ""},
    {title: "Shared", path: "/shared", role: ""},
    {title: "Users", path: "/users", role: "Owner"},
];

export const AppNavbar = () => {
    const navigate = useNavigate();
    const {actions: {logout}, selectors: {isInRole, accessToken}} = useAuthState();
    const location = useLocation();

    return (
        <Header height={80} mb={20}>
            <Container size="xl">
                <Flex
                    direction="row"
                    justify="space-between"
                    align="center"
                    h="100%"
                >
                    <Flex
                        w={300}
                        h="100%"
                        direction="column"
                        justify="center"
                        align="center"
                        style={{cursor: "pointer"}}
                        onClick={() => navigate("/")}
                    >
                        <Text size="xl" weight={700} color="indigo.7">Family Budget</Text>
                        <Text size="xl" weight={700}>Manager</Text>
                    </Flex>
                    <Flex w="100%" h="100%" style={{flexGrow: 1}} justify="center" align="center">
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

                    <Flex w={300} h="100%">
                        <Flex
                            mt={-3}
                            direction="row"
                            justify="center"
                            align="center"
                        >
                            <Mark p={3} mr="sm" color="indigo.1" style={{borderRadius: "5px"}}>
                                {accessToken().email}
                            </Mark>
                        </Flex>
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
