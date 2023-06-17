import {incomeSelectStyles, incomeTextInputStyles} from "@app/budget/components/mantineStyles";
import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Grid, Mark, Select, TextInput, Title} from "@mantine/core";
import {useForm} from "react-hook-form";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {z} from "zod";

interface NewIncomeFormModel {
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

export const AddIncomePopoverContent = ({budgetPlanId}: Props) => {
    const form = useForm<NewIncomeFormModel>({
        defaultValues: {
            name: "",
            date: new Date(),
            amount: 0,
        },
        resolver: zodResolver(formSchema),
    });

    const budgetPlanApi = useBudgetPlanApi();
    const addIncome = budgetPlanApi.commands.addIncome;

    const onValidSubmit: SubmitHandler<NewIncomeFormModel> = (data) => {
        addIncome.mutate({budgetPlanId: budgetPlanId, income: data}, {
            onSuccess: () => Notify.success("Income added!")
        });
    };
    const onInvalidSubmit: SubmitErrorHandler<NewIncomeFormModel> = () => Notify.warning("Fill in all fields or check for errors");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <Grid>
            <Grid.Col span={12}>
                <Title order={4}>Add new <Mark color="green.4" p={2} style={{borderRadius: "5px"}}>income</Mark></Title>
            </Grid.Col>
            <Grid.Col span={6}>
                <TextInput
                    mb={10}
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Income name"
                    type="text"
                    placeholder="Enter income name"
                    required
                    {...form.register("name")}
                    error={form.formState.errors.name?.message}
                    styles={incomeTextInputStyles}
                />

                <TextInput
                    w="100%"
                    size="md"
                    radius="lg"
                    variant="filled"
                    label="Income amount"
                    type="number"
                    placeholder="Enter income amount"
                    required
                    icon="$"
                    {...form.register("amount")}
                    error={form.formState.errors.amount?.message}
                    styles={incomeTextInputStyles}
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
                    label="Income date"
                    placeholder="Enter income date"
                    required
                    {...form.register("date")}
                    error={form.formState.errors.date?.message}
                    styles={incomeTextInputStyles}
                />

                {/*<Controller*/}
                {/*    control={form.control}*/}
                {/*    name="role"*/}
                {/*    render={({field}) => (*/}
                <Select
                    size="md"
                    radius="lg"
                    label="Income category"
                    placeholder="Pick one"
                    variant="filled"
                    data={[
                        {value: "Salary", label: "Salary"},
                        {value: "Investment", label: "Investment"},
                        {value: "Bonus", label: "Bonus"},
                        {value: "Gift", label: "Gift"},
                        {value: "Other", label: "Other"},
                    ]}
                    required
                    styles={incomeSelectStyles}
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
                    color="green.6"
                    radius="lg"
                    fullWidth
                    onClick={onSubmit}
                >
                    Add income
                </Button>
            </Grid.Col>
        </Grid>
    );
};