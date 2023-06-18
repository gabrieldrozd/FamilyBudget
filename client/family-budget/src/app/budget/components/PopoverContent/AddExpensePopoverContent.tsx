import {expenseSelectStyles, expenseTextInputStyles} from "@app/budget/components/mantineStyles";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Grid, Mark, Select, TextInput, Title} from "@mantine/core";
import {Controller, useForm} from "react-hook-form";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {z} from "zod";

interface NewExpenseFormModel {
    name: string;
    date: string;
    amount: string;
    expenseCategory: string;
}

const formSchema = z.object({
    name: z.string().min(3).max(100),
    date: z.string(),
    amount: z.string().min(0),
    expenseCategory: z.enum([
        "Food", "Housing", "Transportation", "Clothing", "Health Care", "Personal", "Education", "Entertainment", "Other"
    ]),
});

interface Props {
    budgetPlanId: string;
}

export const AddExpensePopoverContent = ({budgetPlanId}: Props) => {
    const form = useForm<NewExpenseFormModel>({
        defaultValues: {
            name: "",
            date: "",
            amount: "0",
            expenseCategory: "Food",
        },
        resolver: zodResolver(formSchema),
    });

    const budgetPlanApi = useBudgetPlanApi();
    const addExpense = budgetPlanApi.commands.addExpense;

    const onValidSubmit: SubmitHandler<NewExpenseFormModel> = (data) => {
        addExpense.mutate({
            budgetPlanId: budgetPlanId,
            expense: {
                name: data.name,
                date: new Date(data.date),
                amount: parseFloat(data.amount),
                expenseCategory: data.expenseCategory
            }
        }, {
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

                <Controller
                    control={form.control}
                    name="expenseCategory"
                    render={({field}) => (
                        <Select
                            size="md"
                            radius="lg"
                            label="Expense category"
                            placeholder="Pick one"
                            variant="filled"
                            data={[
                                {value: "Food", label: "Food"},
                                {value: "Housing", label: "Housing"},
                                {value: "Transportation", label: "Transportation"},
                                {value: "Clothing", label: "Clothing"},
                                {value: "Health Care", label: "Health Care"},
                                {value: "Personal", label: "Personal"},
                                {value: "Education", label: "Education"},
                                {value: "Entertainment", label: "Entertainment"},
                                {value: "Other", label: "Other"},
                            ]}
                            required
                            {...field}
                            error={form.formState.errors.expenseCategory?.message}
                            styles={expenseSelectStyles}
                        />
                    )}
                />
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