import Image from 'next/image';
import { Header } from '../components/Header';
import { Avatar } from '../components/Avatar';
import { JobList } from '../components/Job/JobList';
import { jobs } from '../service/data/jobsMok';
import { Button } from '../components/Button';
import { Icon } from '../components/Button/Icon';

export default function Home() {
  return (
    <div className="bg-gray-100 min-h-screen">
      <Header.Root className="page-header bg-gray-700 text-white p-8">
        <Header.Content className="max-w-4xl mx-auto">
          <Header.NavBar className="flex justify-between items-center pb-8">
            <Header.Logo width={208} height={48} />
            <span className="flex gap-2">
              <Image
                src="./images/alert-octagon.svg"
                alt="Alerta"
                width={20}
                height={20}
              />
              Você tem 5 hora livreno seu dia
            </span>
            <Avatar />
          </Header.NavBar>
          <Header.Separator className="h-[1px] my-4 bg-[#4F4F5B]" />
          <Header.Summary className="mt-8 flex items-center justify-between">
            <div className="info flex gap-8 py-8">
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={5}
                text="Projetos ao total"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={1}
                text="Em andamento"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={4}
                text="Encerrados"
              />
            </div>
            <Button.Root
              className="uppercase flex gap-4 bg-orange-400 h-fit px-3 py-2 
                rounded items-center hover:brightness-110 transition-al text-[#FCFDFF]"
            >
              <span className="bg-opacity-10 bg-white rounded p-0.5">
                <Icon height={24} width={24} />
              </span>
              <p className="px-2 text-xs font-bold">Adicionar novo job</p>
            </Button.Root>
          </Header.Summary>
        </Header.Content>
      </Header.Root>
      <div className="max-w-4xl mx-auto -mt-14">
        <main className="">
          <h1 className="sr-only">Trabalhos</h1>

          <div className="cards space-y-2">
            <JobList jobs={jobs} />
          </div>
        </main>
      </div>
    </div>
  );
}
