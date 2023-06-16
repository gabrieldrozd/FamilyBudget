import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Card, Divider, Flex, TextInput, Title} from "@mantine/core";
import {SubmitErrorHandler, SubmitHandler, useForm} from "react-hook-form";
import {z} from "zod";
import {useAuthApi} from "@core/api/hooks/useAuthApi";
import {Notify} from "@core/services/Notify";

interface LoginFormModel {
    email: string;
    password: string;
}

const formSchema = z.object({
    email: z.string().email(),
    password: z.string().min(8),
});

export const StartPage = () => {
    const form = useForm<LoginFormModel>({
        defaultValues: {
            email: "",
            password: "",
        },
        resolver: zodResolver(formSchema),
    });

    const authApi = useAuthApi();
    const loginUser = authApi.commands.login;

    const onValidSubmit: SubmitHandler<LoginFormModel> = (data) => loginUser.mutate(data);
    const onInvalidSubmit: SubmitErrorHandler<LoginFormModel> = () => Notify.info("Fill in all fields");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <Flex
            mt={200}
            direction="column"
            justify="center"
            align="center"
            w="40%"
        >
            <Title>Login to Family Budget Manager</Title>

            <Card p={30} mt={20} w="100%" radius={20} shadow="lg">
                <TextInput
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Email"
                    type="email"
                    placeholder="Enter your email"
                    required
                    {...form.register("email")}
                />

                <Divider color="white.0" my={20} />

                <TextInput
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Password"
                    type="password"
                    placeholder="Enter your password"
                    required
                    {...form.register("password")}
                />

                <Divider variant="solid" my={20} color="gray.3" w="100%" />

                <Button
                    w="100%"
                    size="md"
                    radius="md"
                    color="indigo.5"
                    onClick={onSubmit}
                >
                    Login
                </Button>
            </Card>
        </Flex>
    );
};