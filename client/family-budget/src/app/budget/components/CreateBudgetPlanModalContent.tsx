import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Divider, Flex, Textarea, TextInput} from "@mantine/core";
import {DatePickerInput} from "@mantine/dates";
import {IconCalendar} from "@tabler/icons-react";
import {Controller, useForm} from "react-hook-form";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {z} from "zod";

interface NewBudgetPlanFormModel {
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
}

const formSchema = z.object({
    name: z.string().min(3).max(100),
    description: z.string().min(3).max(300),
    startDate: z.date(),
    endDate: z.date(),
});

interface Props {
    closeModal: () => void;
}

export const CreateBudgetPlanModalContent = ({closeModal}: Props) => {
    const form = useForm<NewBudgetPlanFormModel>({
        defaultValues: {
            name: "",
            description: "",
            startDate: new Date(),
            endDate: new Date(),
        },
        resolver: zodResolver(formSchema),
    });

    const budgetPlanApi = useBudgetPlanApi();
    const createBudgetPlan = budgetPlanApi.commands.createBudgetPlan;

    const onValidSubmit: SubmitHandler<NewBudgetPlanFormModel> = (data) => {
        createBudgetPlan.mutate(data, {
            onSuccess: () => {
                Notify.success("Budget plan created successfully");
                closeModal();
            }
        });
    };
    const onInvalidSubmit: SubmitErrorHandler<NewBudgetPlanFormModel> = () => Notify.warning("Fill in all fields or check for errors");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <>
            <TextInput
                w="100%"
                size="md"
                radius="lg"
                variant="filled"
                label="Budget plan name"
                type="text"
                placeholder="Enter budget plan name"
                required
                {...form.register("name")}
                error={form.formState.errors.name?.message}
            />

            <Divider color="white.0" my={20} />

            <Textarea
                w="100%"
                size="md"
                radius="lg"
                variant="filled"
                minRows={3}
                label="Budget plan description"
                placeholder="Enter budget plan description"
                required
                {...form.register("description")}
                error={form.formState.errors.description?.message}
            />

            <Divider color="white.0" my={20} />

            <Controller
                name="startDate"
                control={form.control}
                render={({field}) => (
                    <DatePickerInput
                        w="100%"
                        icon={<IconCalendar size="1.1rem" stroke={1.5} />}
                        size="md"
                        radius="lg"
                        variant="filled"
                        label="Start date"
                        placeholder="Enter start date"
                        required
                        {...field}
                        error={form.formState.errors.startDate?.message}
                    />
                )}
            />

            <Divider color="white.0" my={20} />

            <Controller
                name="endDate"
                control={form.control}
                render={({field}) => (
                    <DatePickerInput
                        w="100%"
                        icon={<IconCalendar size="1.1rem" stroke={1.5} />}
                        size="md"
                        radius="lg"
                        variant="filled"
                        label="End date"
                        placeholder="Enter end date"
                        required
                        {...field}
                        error={form.formState.errors.endDate?.message}
                    />
                )}
            />

            <Divider variant="solid" my={20} color="gray.3" w="100%" />

            <Flex gap={20}>
                <Button
                    fullWidth
                    size="md"
                    radius="md"
                    color="indigo.5"
                    onClick={onSubmit}
                >
                    Create plan
                </Button>
                <Button
                    fullWidth
                    size="md"
                    radius="md"
                    color="amber.5"
                    onClick={closeModal}
                >
                    Close modal
                </Button>
            </Flex>
        </>
    );
};