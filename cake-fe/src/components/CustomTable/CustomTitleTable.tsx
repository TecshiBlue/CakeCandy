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
  }, {} as any) as T;

  const exportarArchivo = async (tipo: "pdf" | "excel") => {
    try {
      const vista = title.toLowerCase();
      const endpoint = tipo === "excel" 
        ? `/api/export/excel/${vista}` 
        : `/api/export/${vista}`;

      const response = await fetch(endpoint, {
        headers: {
          'Accept': tipo === 'excel' 
            ? 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' 
            : 'application/pdf'
        }
      });

      if (!response.ok) {
        throw new Error(`Error HTTP: ${response.status}`);
      }

      // Verificar el tipo de contenido
      const contentType = response.headers.get('content-type');
      if (
        (tipo === 'excel' && !contentType?.includes('spreadsheetml')) ||
        (tipo === 'pdf' && !contentType?.includes('pdf'))
      ) {
        throw new Error(`Tipo de archivo incorrecto: ${contentType}`);
      }

      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement("a");
      link.href = url;
      link.download = `${vista}.${tipo === "excel" ? "xlsx" : "pdf"}`;
      document.body.appendChild(link);
      link.click();
      
      // Limpieza
      setTimeout(() => {
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      }, 100);
    } catch (err) {
      console.error("Error exportando:", err);
      alert(`Error al exportar archivo: ${err instanceof Error ? err.message : String(err)}`);
    }
  };

  return (
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
            <DropdownMenuItem onClick={() => exportarArchivo("excel")}>
              <FileSpreadsheet className="mr-2 h-4 w-4" />
              Exportar a Excel
            </DropdownMenuItem>
            <DropdownMenuItem onClick={() => exportarArchivo("pdf")}>
              <Printer className="mr-2 h-4 w-4" />
              Exportar a PDF
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </div>
  );
}

export default CustomTitleTable;