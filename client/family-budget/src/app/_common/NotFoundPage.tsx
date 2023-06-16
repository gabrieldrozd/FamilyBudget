import {Button, Container, Group, Text, Title} from "@mantine/core";
import {IconChevronLeft} from "@tabler/icons-react";
import {useNavigate} from "react-router-dom";

export const NotFoundPage = () => {
    const navigate = useNavigate();

    const handleGoBack = () => {
        navigate(-1);
    };

    return (
        <Container>
            <Title order={1}>404</Title>
            <Title order={3}>You have found a secret place.</Title>
            <Text color="dimmed" size="lg" align="center">
                Unfortunately, this is only a 404 page. You may have mistyped the address, or the page has
                been moved to another URL.
            </Text>
            <Group position="center">
                <Button
                    size="lg"
                    color="indigo.5"
                    variant="filled"
                    onClick={handleGoBack}
                    leftIcon={<IconChevronLeft />}
                >
                    Take me back
                </Button>
            </Group>
        </Container>
    );
};