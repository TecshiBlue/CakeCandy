import { ColumnDef } from "@tanstack/react-table";
import { formatColumnName } from "./formatColumnName";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const createDynamicColumns = <T extends Record<string, any>>(
  data: T[]
): ColumnDef<T>[] => {
  if (!data || data.length === 0) {
    return [];
  }

  const keys = Object.keys(data[0]) as (keyof T)[];

  return keys.map((key) => ({
    header: () => formatColumnName(String(key)),
    accessorKey: key as string,
  }));
};
