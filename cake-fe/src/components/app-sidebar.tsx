import * as React from "react"
import {
  House,
  SquareLibrary,
  ShoppingBasket,
  UsersRound,
  Package2,
  Bubbles,
  SwatchBook,
  Shapes,
  Candy,
  CircleDollarSign
} from "lucide-react"


import { NavProjects } from "@/components/nav-projects"
import { NavUser } from "@/components/nav-user"
import {USERNAME, ROLE} from "@/constants/userConstant"
import TeamSwitcher from "@/components/team-switcher"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar"

const data = {
  user: {
    name: USERNAME,
    rol: ROLE,
    avatar: "/avatars/shadcn.jpg",
  },
  navMain: [
    {
      title: "Inicio",
      url: "/",
      icon: House,
      isActive: true,
    },
  ],
  projects: [
    {
      title: "Sabores",
      url: "/sabores",
      icon: Bubbles,
    },
    {
      title: "Categorias",
      url: "/categorias",
      icon: SquareLibrary,
    },
    {
      title: "Marcas",
      url: "/marcas",
      icon: SwatchBook,
    },
    {
      title: "Presentaciones",
      url: "/presentaciones",
      icon: Shapes,
    },
    {
      title: "Dulces",
      url: "/dulces",
      icon: Candy,
    },
    {
      title: "Clientes",
      url: "/clientes",
      icon: UsersRound,
    }, {
      title: "Ventas",
      url: "/ventas",
      icon: CircleDollarSign,
    },
   
  ],
}
export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar collapsible="icon" {...props}>
      <SidebarHeader>
        <TeamSwitcher />
      </SidebarHeader>
      <SidebarContent>
        <NavProjects projects={data.navMain} groupName={"Principal"} />
        <NavProjects projects={data.projects} groupName={"Acciones"} />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={data.user} />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  )
}
