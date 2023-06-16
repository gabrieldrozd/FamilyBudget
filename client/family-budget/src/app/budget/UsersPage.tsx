import {RegisterUserModalContent} from "@app/budget/components/RegisterUserModalContent";
import {useUserApi} from "@core/api/hooks/useUserApi";
import {usePagination} from "@core/context/PaginationContextProvider";
import type {IUserBase} from "@core/models/user";
import {Button, Container, Flex, Modal, ScrollArea, Space, Table} from "@mantine/core";
import {useDisclosure} from "@mantine/hooks";
import {LocalLoader} from "@shared/components/LocalLoader";
import {useAuthState} from "@store/slices/auth/useAuthState";
import {IconPlus, IconSortAscending, IconSortDescending} from "@tabler/icons-react";
import type {ColumnDef, SortingState} from "@tanstack/react-table";
import {createColumnHelper, flexRender, getCoreRowModel, getSortedRowModel, useReactTable} from "@tanstack/react-table";
import {createRef, useState} from "react";

import classes from "./styles/UsersPage.module.scss";

const columnsHelper = createColumnHelper<IUserBase>();
const columns: ColumnDef<IUserBase, any>[] = [
    columnsHelper.accessor(x => x.email, {
        header: "Email",
        cell: value => value.getValue(),
    }),
    columnsHelper.accessor(x => x.firstName, {
        header: "First Name",
        cell: value => value.getValue(),
    }),
    columnsHelper.accessor(x => x.lastName, {
        header: "Last Name",
        cell: value => value.getValue(),
    }),
    columnsHelper.accessor(x => x.role, {
        header: "User Role",
        cell: value => value.getValue(),
    }),
    columnsHelper.accessor(x => x.budgetPlans, {
        header: "Budget Plans Count",
        cell: value => `${value.getValue()}x`,
    }),
    columnsHelper.accessor(x => x.sharedBudgets, {
        header: "Shared Budgets Count",
        cell: value => `${value.getValue()}x`,
    }),
    columnsHelper.display({
        id: "actions",
        header: "Actions",
        cell: props => {
            const authState = useAuthState();
            const isCurrentUser = authState.selectors.accessToken().email === props.row.original.email;

            const userApi = useUserApi();
            const deleteUser = userApi.commands.deleteUser;

            return (
                <Button
                    w="100%"
                    color="amber.6"
                    variant="filled"
                    disabled={isCurrentUser}
                    onClick={() => deleteUser.mutate(props.row.original.externalId)}
                >
                    Remove
                </Button>
            );
        }
    })
];

export const UsersPage = () => {
    const [opened, {open, close}] = useDisclosure(false);

    const pagination = usePagination();
    const userApi = useUserApi();
    const {isLoading, data} = userApi.queries.browseUsers(pagination.model);

    const tableBodyRef = createRef<HTMLTableSectionElement>();
    const [sorting, setSorting] = useState<SortingState>([]);

    const table = useReactTable({
        data: data?.list ?? [],
        columns: columns,
        state: {sorting},
        onSortingChange: setSorting,
        getCoreRowModel: getCoreRowModel(),
        getSortedRowModel: getSortedRowModel()
    });

    if (isLoading) {
        return <LocalLoader loaderSize={100} />;
    }

    return (
        <Container w="100%" size="lg">
            <Modal opened={opened} onClose={close} title="Add new user" centered>
                <RegisterUserModalContent closeModal={close} />
            </Modal>

            <Flex mb={20} justify="flex-end">
                <Button color="indigo.5" variant="filled" onClick={open} rightIcon={<IconPlus />}>
                    Add User
                </Button>
            </Flex>

            <Flex className={classes.tableContainer}>
                <ScrollArea mah="100vh" type="hover" w="100%">
                    <Table className={classes.table}>
                        <thead className={classes.tableHead}>
                        {table.getHeaderGroups().map(headerGroup => (
                            <tr key={headerGroup.id} className={classes.tableHeadRow}>
                                {headerGroup.headers.map(header => (
                                    <th key={header.id} className={classes.tableHeadCell}>
                                        {header.isPlaceholder ? null : (
                                            <Flex
                                                align="center"
                                                justify="center"
                                                {...{
                                                    className: header.column.getCanSort() ? "cursor-pointer select-none" : "",
                                                    onClick: header.column.getToggleSortingHandler(),
                                                }}
                                            >
                                                {flexRender(
                                                    header.column.columnDef.header,
                                                    header.getContext()
                                                )}
                                                <Space w={5} />
                                                {{
                                                    asc: <IconSortAscending size={20} />,
                                                    desc: <IconSortDescending size={20} />,
                                                }[header.column.getIsSorted() as string] ?? null}
                                            </Flex>
                                        )}
                                    </th>
                                ))}
                            </tr>
                        ))}
                        </thead>
                        <tbody ref={tableBodyRef} tabIndex={0} className={classes.tableBody}>
                        {table.getRowModel().rows.map(row => (
                            <tr key={row.id} className={classes.tableBodyRow}>
                                {row.getVisibleCells().map(cell => (
                                    <td key={cell.id} className={classes.tableBodyCell}>
                                        {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                    </td>
                                ))}
                            </tr>
                        ))}
                        </tbody>
                    </Table>
                </ScrollArea>
            </Flex>
        </Container>
    );
};