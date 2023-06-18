import {useBudgetPlanApi} from "@core/api/hooks/useBudgetPlanApi";
import {Notify} from "@core/services/Notify";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button, Grid, Mark, MultiSelect, Select, TextInput, Title} from "@mantine/core";
import {Controller, useForm} from "react-hook-form";
import type {SubmitErrorHandler, SubmitHandler} from "react-hook-form";
import {z} from "zod";
import {expenseSelectStyles, expenseTextInputStyles, incomeSelectStyles} from "@app/budget/components/mantineStyles";
import {IUserBase} from "@core/models/user";

interface ShareFormModel {
    userIds: string[];
}

const formSchema = z.object({
    userIds: z.array(z.string())
});

interface Props {
    availableUsers: IUserBase[];
    shareBudgetPlan: (userIds: string[]) => void;
}

export const ShareBudgetPlanPopoverContent = ({availableUsers, shareBudgetPlan}: Props) => {
    const form = useForm<ShareFormModel>({
        defaultValues: {
            userIds: [],
        },
        resolver: zodResolver(formSchema),
    });

    const onValidSubmit: SubmitHandler<ShareFormModel> = (data) => {
        console.log(data);
        shareBudgetPlan(data.userIds);
    };
    const onInvalidSubmit: SubmitErrorHandler<ShareFormModel> = () => Notify.warning("Select at least one user");
    const onSubmit = form.handleSubmit(onValidSubmit, (errors) => onInvalidSubmit(errors));

    return (
        <Grid>
            <Grid.Col span={12}>
                <Title order={4}>Share budget plan with:</Title>
            </Grid.Col>
            <Grid.Col span={12}>
                <Controller
                    control={form.control}
                    name="userIds"
                    render={({field}) => (
                        <MultiSelect
                            multiple
                            size="md"
                            radius="lg"
                            label="Users"
                            placeholder="Select users"
                            variant="filled"
                            data={availableUsers.map((user) => ({
                                value: user.externalId,
                                label: `${user.email} (${user.firstName} ${user.lastName})`,
                            }))}
                            required
                            {...field}
                            error={form.formState.errors.userIds?.message}
                            styles={{
                                wrapper: {
                                    border: "none",
                                },
                            }}
                        />
                    )}
                />
            </Grid.Col>

            <Grid.Col span={12}>
                <Button
                    type="submit"
                    variant="light"
                    color="blue.6"
                    radius="lg"
                    fullWidth
                    onClick={onSubmit}
                >
                    Share budget plan
                </Button>
            </Grid.Col>
        </Grid>
    );
};