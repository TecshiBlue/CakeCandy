import { PlusCircle } from "lucide-react";
import { Button } from "@/components/ui/button";
import FormModal from "./FormModal";

interface CustomTitleTableProps<T> {
  title: string;
  onAdd: (item: T) => void;
  addFields: { key: keyof T; label: string; placeholder?: string; type?: string }[];
}

export function CustomTitleTable<T>({
  title,
  onAdd,
  addFields,
}: CustomTitleTableProps<T>) {
  const emptyData = addFields.reduce((acc, field) => {
    acc[field.key] = field.type === "number" ? 0 : "";
    return acc;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  }, {} as any) as T;

  return (
    <div className="flex items-center justify-between">
      <h1 className="text-2xl font-bold">{title}</h1>
      <div className="flex items-center gap-4">
        <FormModal
          trigger={
            <Button size="sm">
              <PlusCircle className="mr-2 h-4 w-4" />
              Agregar
            </Button>
          }
          title={`Agregar ${title}`}
          initialData={emptyData}
          fields={addFields}
          onSave={onAdd}
        />
      </div>
    </div>
  );
}

export default CustomTitleTable;
