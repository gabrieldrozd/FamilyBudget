import type {IPaginationRequest} from "@core/models/pagination";
import type {ReactNode} from "react";
import {createContext, useContext, useMemo, useState} from "react";

export interface IPaginationModel {
    model: IPaginationRequest;
    setPageIndex: (index: number) => void;
    setPageSize: (size: number) => void;
    setIsAscending: (isAscending: boolean) => void;
}

const PaginationContext = createContext<IPaginationModel>({} as IPaginationModel);
export const usePagination = () => useContext(PaginationContext);

interface Props {
    children: ReactNode;
}

export const PaginationContextProvider = ({children}: Props) => {
    const [pagination, setPagination] = useState<IPaginationRequest>({
        pageIndex: 1,
        pageSize: 10,
        isAscending: true,
    });

    const handleSetPageIndex = async (index: number) => setPagination({...pagination, pageIndex: index});
    const handleSetPageSize = async (size: number) => setPagination({...pagination, pageSize: size});
    const handleSetIsAscending = async (isAscending: boolean) => setPagination({...pagination, isAscending: isAscending});

    const contextObject = useMemo(() => ({
        model: pagination,
        setPageIndex: handleSetPageIndex,
        setPageSize: handleSetPageSize,
        setIsAscending: handleSetIsAscending,
    }), [pagination]);

    return (
        <PaginationContext.Provider value={contextObject}>
            {children}
        </PaginationContext.Provider>
    );
};