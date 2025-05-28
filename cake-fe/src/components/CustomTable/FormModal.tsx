import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ReactNode, useState, useEffect } from "react";

interface FormField<T> {
  key: keyof T;
  label: string;
  placeholder?: string;
  type?: string; 
  options?: { value: string; label: string }[]; 
}

export interface FormModalProps<T> {
  trigger: ReactNode;
  onSave?: (values: T) => void;
  initialData?: T;
  readonly?: boolean;
  title: string;
  fields: FormField<T>[]; 
}

function FormModal<T>({
  trigger,
  onSave,
  initialData,
  readonly,
  title,
  fields,
}: FormModalProps<T>) {
  const [open, setOpen] = useState(false);
  const [values, setValues] = useState<Partial<T>>({});

  useEffect(() => {
    if (initialData) setValues(initialData);
  }, [initialData]);

  function handleChange(key: keyof T, value: string) {
    setValues((prev) => ({ ...prev, [key]: value }));
  }

  function handleSave() {
    if (!onSave) return;
    onSave(values as T);
    setOpen((prev) => !prev);
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>{trigger}</DialogTrigger>
      <DialogContent className="sm:max-w-lg">
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <div className="grid gap-4 py-4">
          {fields.map((f) => {
            const value = values[f.key];
            
            if (f.type === "select" && f.options) {
              const options = f.options || [];
              const selectedValue = value ? String(value) : "";
              
              console.log("Rendering select with options:", options);
              
              return (
                <div key={String(f.key)} className="space-y-2">
                  <label className="block text-sm font-medium">{f.label}</label>
                  <select
                    value={selectedValue}
                    onChange={(e) => handleChange(f.key, e.target.value)}
                    className="w-full rounded-md border border-input bg-background px-3 py-2"
                    disabled={readonly}
                  >
                    <option value="">Selecciona una opción</option>
                    {options.map((option) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                </div>
              );
            }
            
            return (
              <div key={String(f.key)} className="space-y-2">
                <label className="block text-sm font-medium">{f.label}</label>
                <Input
                  value={value !== undefined ? String(value) : ""}
                  placeholder={f.placeholder}
                  type={f.type}
                  onChange={(e) => handleChange(f.key, e.target.value)}
                  className="w-full"
                  disabled={readonly}
                />
              </div>
            );
          })}
        </div>
        <DialogFooter>
          {!readonly ? (
            <>
              <Button
                variant="outline"
                onClick={() => setOpen((prev) => !prev)}
              >
                Cancelar
              </Button>
              <Button onClick={handleSave}>Guardar</Button>
            </>
          ) : (
            <Button variant="outline" onClick={() => setOpen((prev) => !prev)}>
              Cerrar
            </Button>
          )}
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

export default FormModal;