import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Grid, Mark, Select, TextInput, Title} from "@mantine/core";
import {DatePickerInput} from "@mantine/dates";
import {IconCalendar} from "@tabler/icons-react";
import {Controller, useForm} from "react-hook-form";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {z} from "zod";
import {expenseSelectStyles, expenseTextInputStyles, incomeTextInputStyles} from "@app/budget/components/mantineStyles";

interface NewExpenseFormModel {
    name: string;
    date: Date;
    amount: number;
}

const formSchema = z.object({
    name: z.string().min(3).max(100),
    date: z.date(),
    amount: z.number().min(0),
});

interface Props {
    budgetPlanId: string;
}

export const AddExpensePopoverContent = ({budgetPlanId}: Props) => {
    const form = useForm<NewExpenseFormModel>({
        defaultValues: {
            name: "",
            date: new Date(),
            amount: 0,
        },
        resolver: zodResolver(formSchema),
    });

    const budgetPlanApi = useBudgetPlanApi();
    const addExpense = budgetPlanApi.commands.addExpense;

    const onValidSubmit: SubmitHandler<NewExpenseFormModel> = (data) => {
        addExpense.mutate({budgetPlanId: budgetPlanId, expense: data}, {
            onSuccess: () => Notify.success("Expense added!")
        });
    };
    const onInvalidSubmit: SubmitErrorHandler<NewExpenseFormModel> = () => Notify.warning("Fill in all fields or check for errors");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <Grid>
            <Grid.Col span={12}>
                <Title order={4}>Add new <Mark color="amber.4" p={2} style={{borderRadius: "5px"}}>expense</Mark></Title>
            </Grid.Col>
            <Grid.Col span={6}>
                <TextInput
                    mb={10}
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Expense name"
                    type="text"
                    placeholder="Enter expense name"
                    required
                    {...form.register("name")}
                    error={form.formState.errors.name?.message}
                    styles={expenseTextInputStyles}
                />

                <TextInput
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Expense amount"
                    type="number"
                    placeholder="Enter expense amount"
                    required
                    icon="$"
                    {...form.register("amount")}
                    error={form.formState.errors.amount?.message}
                    styles={expenseTextInputStyles}
                />
            </Grid.Col>
            <Grid.Col span={6}>
                <TextInput
                    mb={10}
                    w="100%"
                    size="md"
                    radius="lg"
                    type="date"
                    variant="filled"
                    label="Expense date"
                    placeholder="Enter expense date"
                    required
                    {...form.register("date")}
                    error={form.formState.errors.date?.message}
                    styles={expenseTextInputStyles}
                />

                {/*<Controller*/}
                {/*    control={form.control}*/}
                {/*    name="role"*/}
                {/*    render={({field}) => (*/}
                <Select
                    size="md"
                    radius="lg"
                    label="Expense category"
                    placeholder="Pick one"
                    variant="filled"
                    data={[
                        {value: "House", label: "House"},
                        {value: "Utilities", label: "Utilities"},
                        {value: "Entertainment", label: "Entertainment"},
                        {value: "Health", label: "Health"},
                        {value: "Car", label: "Car"},
                        {value: "Food", label: "Food"},
                        {value: "Clothes", label: "Clothes"},
                        {value: "Other", label: "Other"},
                    ]}
                    required
                    styles={expenseSelectStyles}
                />
                {/*{...field}*/}
                {/*error={form.formState.errors.role?.message}*/}
                {/*    )}*/}
                {/*/>*/}
            </Grid.Col>

            <Grid.Col span={12}>
                <Button
                    type="submit"
                    variant="light"
                    color="amber.6"
                    radius="lg"
                    fullWidth
                    onClick={onSubmit}
                >
                    Add Expense
                </Button>
            </Grid.Col>
        </Grid>
    );
};