import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Sabor = {
  idSabor: number;
  nombreSabor: string;
};

const formFields = [
  {
    key: "nombreSabor",
    label: "Nombre de Sabor",
    placeholder: "Ej: Salado",
  },
] satisfies { key: keyof Sabor; label: string; placeholder?: string }[];


const SaborPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: sabores = [],
    loading,
    error,
    refetch,
  } = useCallApi<Sabor[]>({
    url: "/api/sabor",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Sabor, "idSabor">) => {
    try {
      setIsSubmitting(true);
      await callApi<Sabor, typeof newItem>({
        url: "/api/sabor",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar el sabor:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Sabor) => {
    try {
      setIsSubmitting(true);
      await callApi<Sabor, Sabor>({
        url: `/api/sabor/${updatedItem.idSabor}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar el sabor:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Sabor) => {
    if (!confirm(`¿Eliminar "${item.nombreSabor}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/sabor/${item.idSabor}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar el sabor:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = async (item: Sabor) => {
     try {
      setIsSubmitting(true);
      await callApi<Sabor, Sabor>({
        url: `/api/sabor/${item.idSabor}`,
        methodType: "GET",
        body: item,
      });
      await refetch();
    } catch (error) {
      console.error("Error al ver el sabor:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;

  if (!sabores || sabores.length === 0) {
    return (
    <CustomTable
      tableTitle="Sabores"
        data={[]}
        columns={[]}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
  }

  const columns = createDynamicColumns(sabores);
  return (
    <CustomTable
      tableTitle="Sabores"
      data={sabores}
      columns={columns}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default SaborPageContainer;
