import type {IPaginationModel} from "@core/context/PaginationContextProvider";
import type {IPaginatedList} from "@core/models/pagination";
import {Flex, Group, Mark, Pagination, Select, Space, Text, Title} from "@mantine/core";
import type {QueryObserverResult, RefetchOptions, RefetchQueryFilters} from "@tanstack/react-query";

interface Props {
    data: IPaginatedList<any>;
    pagination: IPaginationModel;
    refetch: <TPageData>(options?: (RefetchOptions & RefetchQueryFilters<TPageData>) | undefined) => Promise<QueryObserverResult<any>>;
}

export const Paginator = (
    {data, pagination, refetch}: Props
) => {
    const totalPages = Math.ceil((data?.pagination.totalItems ?? 0) / pagination.model.pageSize);

    const handlePageSizeChange = async (size: string | null) => {
        if (size) {
            const newPageSize = parseInt(size);
            await pagination.setPageSize(newPageSize);
            await refetch();
        }
    };

    const handlePageChange = async (page: number) => {
        await pagination.setPageIndex(page);
        await refetch();
    };

    const handleNextPage = async () => {
        if (data && data.pagination.hasNextPage) {
            const newPageIndex = pagination.model.pageIndex + 1;
            await pagination.setPageIndex(newPageIndex);
            await refetch();
        }
    };

    const handlePreviousPage = async () => {
        if (data && data.pagination.hasPreviousPage) {
            const newPageIndex = pagination.model.pageIndex - 1;
            await pagination.setPageIndex(newPageIndex);
            await refetch();
        }
    };

    return (
        <Group
            mt={20}
            p={15}
            bg="indigo.2"
            position="apart"
            align="center"
            style={{
                borderRadius: "20px",
            }}
        >
            {data && data.pagination.totalItems > 0 ? (
                <Title order={6}>
                    Showing {" "}
                    <Mark color="white" p={3} style={{borderRadius: "5px"}}>
                        {data.pagination.count}
                    </Mark>
                    {" "}out of{" "}
                    <Mark color="white" p={3} style={{borderRadius: "5px"}}>
                        {data.pagination.totalItems}
                    </Mark>
                    {" "}entries
                </Title>

            ) : (
                <Title order={6}>No entries found</Title>
            )}

            {data && data.pagination.totalItems > 0 && (
                <Group>
                    <Flex align="center">
                        <Text fw={500}>Page size</Text>
                        <Space w={5} />
                        <Select
                            w={80}
                            size="md"
                            variant="default"
                            value={pagination.model.pageSize.toString()}
                            data={[
                                {value: "5", label: "5"},
                                {value: "10", label: "10"},
                                {value: "25", label: "25"},
                            ]}
                            onChange={value => handlePageSizeChange(value)}
                            styles={(theme) => ({
                                item: {
                                    "&[data-selected]": {
                                        "&, &:hover": {
                                            backgroundColor: theme.colors.indigo[5],
                                            color: theme.colors.white[0],
                                            fontWeight: 500,
                                        },
                                    },
                                    "&[data-hovered]": {},
                                },
                                input: {
                                    "&:focus": {
                                        boxShadow: `0 0 0 1px ${theme.colors.indigo[5]}`,
                                        borderColor: theme.colors.indigo[5],
                                    },
                                },
                            })}
                        />
                    </Flex>
                    <Space w={10} />
                    <Pagination
                        size="md"
                        color="indigo.5"
                        total={totalPages}
                        value={pagination.model.pageIndex}
                        onNextPage={handleNextPage}
                        onPreviousPage={handlePreviousPage}
                        onChange={handlePageChange}
                    />
                </Group>
            )}
        </Group>
    );
};