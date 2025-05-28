import {
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import {JSX} from "react";
import {useNavigate} from "react-router-dom";

type CardShortcutProps = {
  title: string;
  icon: JSX.Element;
  url: string;
}

const CardShortcut = (
  { 
    title,
    icon,
    url,
  }:CardShortcutProps ) => {
    const navigate = useNavigate();

    const handleClick = (url: string) => {
        navigate(url);
    };

    return (
    <Card className="@container/card">
      <CardHeader className="relative">
        <CardDescription className=" flex justify-center">
          Consulta y Administra
        </CardDescription>
        <CardTitle className="flex justify-center gap-1 items-center text-2xl font-semibold tabular-nums @md/card:text-3xl">
          {title} {icon}
        </CardTitle>
      </CardHeader>
      <CardFooter className="flex-col items-center gap-1 text-sm">
        <Button
            onClick={() => handleClick(url)}
        >
            Ver más
        </Button>
      </CardFooter>
    </Card>
  );
};

export default CardShortcut;
