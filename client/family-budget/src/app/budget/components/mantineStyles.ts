import {colors} from "@shared/colors";

// Expense
export const expenseTextInputStyles = {
    input: {
        "&:focus": {
            borderColor: colors.amber500,
            borderWidth: 2,
        }
    }
};

export const expenseSelectStyles = {
    input: {
        "&:focus": {
            borderColor: colors.amber500,
            borderWidth: 2,
        }
    },
    item: {
        "&[data-selected]": {
            "&, &:hover": {
                backgroundColor: colors.amber500,
                color: colors.white,
            },
        },
    },
};

// Income
export const incomeTextInputStyles = {
    input: {
        "&:focus": {
            borderColor: colors.green500,
            borderWidth: 2,
        }
    }
};
export const incomeSelectStyles = {
    input: {
        "&:focus": {
            borderColor: colors.green500,
            borderWidth: 2,
        }
    },
    item: {
        "&[data-selected]": {
            "&, &:hover": {
                backgroundColor: colors.green500,
                color: colors.white,
            },
        },
    }
};