import {useUserApi} from "@core/api/hooks/useUserApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Divider, Select, TextInput} from "@mantine/core";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {Controller, useForm} from "react-hook-form";
import {z} from "zod";

interface RegisterUserFormModel {
    email: string;
    firstName: string;
    lastName: string;
    role: string;
}

const formSchema = z.object({
    email: z.string().email(),
    firstName: z.string().min(2),
    lastName: z.string().min(2),
    role: z.enum(["Member", "Admin"]),
});

interface Props {
    closeModal: () => void;
}

export const RegisterUserModalContent = ({closeModal}: Props) => {
    const form = useForm<RegisterUserFormModel>({
        defaultValues: {
            email: "",
            firstName: "",
            lastName: "",
            role: "",
        },
        resolver: zodResolver(formSchema),
    });

    const userApi = useUserApi();
    const registerUser = userApi.commands.registerUser;

    const onValidSubmit: SubmitHandler<RegisterUserFormModel> = (data) => {
        registerUser.mutate(data, {
            onSuccess: () => {
                Notify.success("User registered successfully");
                closeModal();
            }
        });
    };
    const onInvalidSubmit: SubmitErrorHandler<RegisterUserFormModel> = () => Notify.warning("Fill in all fields or check for errors");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <>
            <TextInput
                w="100%"
                size="md"
                radius="lg"
                variant="filled"
                label="Email"
                type="email"
                placeholder="Enter email"
                required
                {...form.register("email")}
                error={form.formState.errors.email?.message}
            />

            <Divider color="white.0" my={20} />

            <TextInput
                w="100%"
                size="md"
                radius="lg"
                variant="filled"
                label="First name"
                type="text"
                placeholder="Enter first name"
                required
                {...form.register("firstName")}
                error={form.formState.errors.firstName?.message}
            />

            <Divider color="white.0" my={20} />

            <TextInput
                w="100%"
                size="md"
                radius="lg"
                variant="filled"
                label="Last name"
                type="text"
                placeholder="Enter last name"
                required
                {...form.register("lastName")}
                error={form.formState.errors.lastName?.message}
            />

            <Divider color="white.0" my={20} />

            <Controller
                control={form.control}
                name="role"
                render={({field}) => (
                    <Select
                        label="User role"
                        placeholder="Pick one"
                        data={[
                            {value: "Member", label: "Member"},
                            {value: "Owner", label: "Owner"},
                        ]}
                        required
                        {...field}
                        error={form.formState.errors.role?.message}
                    />
                )}
            />

            <Divider variant="solid" my={20} color="gray.3" w="100%" />

            <Button
                w="100%"
                size="md"
                radius="md"
                color="indigo.5"
                onClick={onSubmit}
            >
                Register
            </Button>
        </>
    );
};