import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Presentacion = {
  idPresentacion: number;
  nombrePresentacion: string;
};

const formFields = [
  {
    key: "nombrePresentacion",
    label: "Nombre de Presentacion",
    placeholder: "Ingresa tu Presentacion",
  },
] satisfies { key: keyof Presentacion; label: string; placeholder?: string }[];


const PresentacionPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: datas = [], 
    loading,
    error,
    refetch,
  } = useCallApi<Presentacion[]>({
    url: "/api/presentacion",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Presentacion, "idPresentacion">) => {
    try {
      setIsSubmitting(true);
      await callApi<Presentacion, typeof newItem>({
        url: "/api/presentacion",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar la Presentacion:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Presentacion) => {
    try {
      setIsSubmitting(true);
      await callApi<Presentacion, Presentacion>({
        url: `/api/presentacion/${updatedItem.idPresentacion}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar Presentacion:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Presentacion) => {
    if (!confirm(`¿Eliminar "${item.nombrePresentacion}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/presentacion/${item.idPresentacion}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar Presentacion:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Presentacion) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;

  if (!datas || datas.length === 0) {
    return (
    <CustomTable
      tableTitle="Presentaciones"
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

  const columns = createDynamicColumns(datas); 
  return (
    <CustomTable
      tableTitle="Presentaciones"
      data={datas} 
      columns={columns}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default PresentacionPageContainer;
