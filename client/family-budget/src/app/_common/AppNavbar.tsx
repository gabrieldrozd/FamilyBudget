import {
    Container,
    Flex,
    Group,
    Header,
    Paper,
    Text,
    UnstyledButton,
} from "@mantine/core";
import {colors} from "@shared/colors";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {Link, useLocation} from "react-router-dom";

const navItems = [
    {title: "Budget", path: "/", role: ""},
    {title: "Shared", path: "/shared", role: ""},
    {title: "Users", path: "/users", role: "Owner"},
];

export const AppNavbar = () => {
    const {actions: {logout}} = useAuthState();
    const location = useLocation();

    return (
        <Header height={80} mb={20}>
            <Container size="md">
                <Group position="apart" h="100%">
                    <Flex h="100%" direction="column" justify="center" align="center">
                        <Text size="xl" weight={700} color="indigo.7">Family Budget</Text>
                        <Text size="xl" weight={700}>Manager</Text>
                    </Flex>
                    <Flex style={{height: "100%"}}>
                        {navItems.map((item) => {
                            if (item.role && item.role !== "Owner") {
                                // TODO: Conditionally render Users nav item based on user role
                                // only Owner can see all users and manage them (Delete, Update)
                                // Member can see other users but only when sharing budget

                                // TODO: Conditionally render Users nav item based on user role
                                // only Owner can see all users and manage them (Delete, Update)
                                // Member can see other users but only when sharing budget

                                // TODO: Conditionally render Users nav item based on user role
                                // only Owner can see all users and manage them (Delete, Update)
                                // Member can see other users but only when sharing budget

                                // TODO: Conditionally render Users nav item based on user role
                                // only Owner can see all users and manage them (Delete, Update)
                                // Member can see other users but only when sharing budget

                                // TODO: Conditionally render Users nav item based on user role
                                // only Owner can see all users and manage them (Delete, Update)
                                // Member can see other users but only when sharing budget
                                return (
                                    <Paper key={item.path} py="sm" px="xs" my="xs" style={{borderRadius: 4}}>
                                        <Link
                                            to={item.path}
                                            style={{
                                                display: "inline-block",
                                                textDecoration: "none",
                                                padding: "8px",
                                                borderRadius: 4,
                                                backgroundColor: location.pathname === item.path ? colors.indigo200 : "transparent",
                                            }}
                                        >
                                            {item.title}
                                        </Link>
                                    </Paper>
                                );
                            } else {
                                return (
                                    <Paper key={item.path} py="sm" px="xs" my="xs" style={{borderRadius: 4}}>
                                        <Link
                                            to={item.path}
                                            style={{
                                                display: "inline-block",
                                                textDecoration: "none",
                                                padding: "8px",
                                                borderRadius: 4,
                                                backgroundColor: location.pathname === item.path ? colors.indigo200 : "transparent",
                                            }}
                                        >
                                            {item.title}
                                        </Link>
                                    </Paper>
                                );
                            }
                        })}
                        <Paper py="sm" my="xs" style={{borderRadius: 4}}>
                            <UnstyledButton onClick={logout} style={{textDecoration: "none", padding: "8px", display: "inline-block"}}>
                                Logout
                            </UnstyledButton>
                        </Paper>
                    </Flex>
                </Group>
            </Container>
        </Header>
    );
};
