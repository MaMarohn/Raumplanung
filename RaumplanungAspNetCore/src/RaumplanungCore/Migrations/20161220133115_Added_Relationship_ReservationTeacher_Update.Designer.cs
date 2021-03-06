﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RaumplanungCore.Database;

namespace RaumplanungCore.Migrations
{
    [DbContext(typeof(ReservationContext))]
    [Migration("20161220133115_Added_Relationship_ReservationTeacher_Update")]
    partial class Added_Relationship_ReservationTeacher_Update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RaumplanungCore.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TeacherId");

                    b.HasKey("ReservationId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RaumplanungCore.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RaumplanungCore.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RaumplanungCore.Models.Reservation", b =>
                {
                    b.HasOne("RaumplanungCore.Models.Teacher", "Teacher")
                        .WithMany("Reservations")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
