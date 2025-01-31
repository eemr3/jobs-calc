'use client';
import { useMutation } from '@apollo/client';
import Image from 'next/image';
import Link from 'next/link';
import { useState } from 'react';
import toast from 'react-hot-toast';
import { calculateFreeHours, countProjects } from '../../libs/calculate';
import { Job, JobMainProps } from '../../libs/types/typesAndInterfaces';
import { DELETE_JOB } from '../../service/graphql/mutations/jobMutation';
import { Avatar } from '../Avatar';
import { Icon } from '../Button/Icon';
import { Header } from '../Header';
import { Cards } from './Cards';

export function JobMain({ jobsData, planningData, user }: JobMainProps) {
  const [deleteJob] = useMutation(DELETE_JOB);
  const [jobs, setJobs] = useState<Job[]>(jobsData);

  const handleDelete = async (jobId: string) => {
    try {
      await deleteJob({
        variables: {
          jobId: jobId,
        },
      });
      setJobs((prevJobs) => prevJobs.filter((job) => job.jobId !== jobId));
      return toast.success('Job excluído com sucesso!');
    } catch (error) {
      return toast.error('Houve um erro ao tentar apagar o projeto.');
    }
  };

  return (
    <div className="bg-gray-100 min-h-screen">
      <Header.Root className="page-header bg-gray-700 text-white p-8">
        <Header.Content className="max-w-4xl mx-auto">
          <Header.NavBar className="flex justify-between items-center pb-8">
            <Header.Logo width={208} height={48} />
            <span className="flex gap-2">
              <Image
                src="/images/alert-octagon.svg"
                alt="Alerta"
                width={20}
                height={20}
              />
              {`você tem ${
                jobs.length > 0 ? calculateFreeHours(jobs, planningData) : 0
              } horas livres no seu dia.`}
            </span>
            <Avatar user={user} />
          </Header.NavBar>
          <Header.Separator className="h-[1px] my-4 bg-[#4F4F5B]" />
          <Header.Summary className="mt-8 flex items-center justify-between">
            <div className="info flex gap-8 py-8">
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).totalTjobs : 0}
                text="Projetos ao total"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).jobsDone : 0}
                text="Em andamento"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).jobsfinish : 0}
                text="Encerrados"
              />
            </div>
            {planningData.planningId !== '00000000-0000-0000-0000-000000000000' ? (
              <Link
                href="/project/new"
                className="uppercase flex gap-4 bg-orange-400 h-fit px-3 py-2 
                rounded items-center hover:brightness-110 transition-al text-[#FCFDFF]"
              >
                <span className="bg-opacity-10 bg-white rounded p-0.5">
                  <Icon.Plus24 height={24} width={24} />
                </span>
                <p className="px-2 text-xs font-bold">Adicionar novo job</p>
              </Link>
            ) : (
              <Link
                href="/profile"
                className="uppercase flex gap-4 bg-orange-400 h-fit px-3 py-2 
              rounded items-center hover:brightness-110 transition-al text-[#FCFDFF]"
              >
                {' '}
                <span className="bg-opacity-10 bg-white rounded p-0.5">
                  <Icon.Planning height={24} width={24} />
                </span>
                <p className="px-2 text-xs font-bold">Criar seu planejamento</p>
              </Link>
            )}
          </Header.Summary>
        </Header.Content>
      </Header.Root>
      <div className="max-w-4xl mx-auto -mt-14">
        <main className="">
          <h1 className="sr-only">Trabalhos</h1>
          <div className="py-6 space-y-2">
            {jobs.length > 0
              ? jobs.map((job) => (
                  <Cards
                    key={job.jobId}
                    jobId={job.jobId}
                    name={job.name}
                    remainingDays={job.remainingDays}
                    status={job.status}
                    valueJob={job.valueJob}
                    onDelete={handleDelete}
                  />
                ))
              : null}
          </div>
        </main>
      </div>
    </div>
  );
}
