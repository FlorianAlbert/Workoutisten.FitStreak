﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workoutisten.FitStreak.Server.Database;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    [DbContext(typeof(FitStreakDbContext))]
    partial class FitStreakDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EmailUser", b =>
                {
                    b.Property<Guid>("EmailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiversId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmailsId", "ReceiversId");

                    b.HasIndex("ReceiversId");

                    b.ToTable("EmailUser");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<Guid>("MyFriendsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersIAmFriendOfId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MyFriendsId", "UsersIAmFriendOfId");

                    b.HasIndex("UsersIAmFriendOfId");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Account.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Account.FriendshipRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RequestedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RequestingUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestingUserId");

                    b.HasIndex("RequestedUserId", "RequestingUserId")
                        .IsUnique();

                    b.ToTable("FriendshipRequest");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Account.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PasswordForgottenKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PasswordForgottenKey")
                        .IsUnique()
                        .HasFilter("[PasswordForgottenKey] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.ExerciseEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("WorkoutEntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutEntryId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseEntry");

                    b.HasDiscriminator<string>("Category");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.WorkoutEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WorkoutEntry");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.WorkoutExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("ExerciseId", "WorkoutId")
                        .IsUnique();

                    b.ToTable("WorkoutExercise");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.CardioExerciseEntry", b =>
                {
                    b.HasBaseType("Workoutisten.FitStreak.Server.Model.Excercise.ExerciseEntry");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<long>("Duration")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("Cardio");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.StrengthExerciseEntry", b =>
                {
                    b.HasBaseType("Workoutisten.FitStreak.Server.Model.Excercise.ExerciseEntry");

                    b.Property<int>("Repetitions")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Strength");
                });

            modelBuilder.Entity("EmailUser", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.Email", null)
                        .WithMany()
                        .HasForeignKey("EmailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", null)
                        .WithMany()
                        .HasForeignKey("ReceiversId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", null)
                        .WithMany()
                        .HasForeignKey("MyFriendsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UsersIAmFriendOfId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Account.FriendshipRequest", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", "RequestedUser")
                        .WithMany("IngoingFriendshipRequests")
                        .HasForeignKey("RequestedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", "RequestingUser")
                        .WithMany("OutgoingFriendshipRequests")
                        .HasForeignKey("RequestingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestedUser");

                    b.Navigation("RequestingUser");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.Exercise", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", "Creator")
                        .WithMany("Exercises")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.ExerciseEntry", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Excercise.Exercise", "Exercise")
                        .WithMany("ExerciseEntries")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Workout.WorkoutEntry", "WorkoutEntry")
                        .WithMany("ExerciseEntries")
                        .HasForeignKey("WorkoutEntryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Workout.Workout", "Workout")
                        .WithMany("WorkoutContextExerciseEntries")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Exercise");

                    b.Navigation("Workout");

                    b.Navigation("WorkoutEntry");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.Workout", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Account.User", "Creator")
                        .WithMany("Workouts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.WorkoutExercise", b =>
                {
                    b.HasOne("Workoutisten.FitStreak.Server.Model.Excercise.Exercise", "Exercise")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Workoutisten.FitStreak.Server.Model.Workout.Workout", "Workout")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Account.User", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("IngoingFriendshipRequests");

                    b.Navigation("OutgoingFriendshipRequests");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Excercise.Exercise", b =>
                {
                    b.Navigation("ExerciseEntries");

                    b.Navigation("WorkoutExercises");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.Workout", b =>
                {
                    b.Navigation("WorkoutContextExerciseEntries");

                    b.Navigation("WorkoutExercises");
                });

            modelBuilder.Entity("Workoutisten.FitStreak.Server.Model.Workout.WorkoutEntry", b =>
                {
                    b.Navigation("ExerciseEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
