import { PlusCircle, FileSpreadsheet, Printer } from "lucide-react";
import { Button } from "@/components/ui/button";
import FormModal from "./FormModal";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "../ui/dropdown-menu";

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
    <>
    <div className="flex items-center">
      
      <h1 className="text-2xl font-bold">{title}</h1>
     
      <div className="flex justify-end w-100 mx-6">     
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
    <div>
       <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                
               Exportar...
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem onClick={() => {}}>
                <FileSpreadsheet className="mr-2 h-4 w-4" />
                Exportar a Excel
              </DropdownMenuItem>
              <DropdownMenuItem onClick={() => {}}>
                <Printer className="mr-2 h-4 w-4" />
                Exportar a PDF
              </DropdownMenuItem>
              
            </DropdownMenuContent>
          </DropdownMenu>
          </div> 
        </div>
        </>
  );
}

export default CustomTitleTable;
