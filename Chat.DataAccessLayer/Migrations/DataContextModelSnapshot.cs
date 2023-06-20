﻿// <auto-generated />
using System;
using Chat.DataAccessLayer.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.DataAccessLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Chat.Domain.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 1L, null, null, null, null, null);

                    b.Property<int>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.Property<int>("RecieverId")
                        .HasColumnType("integer")
                        .HasColumnName("reciever_id");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer")
                        .HasColumnName("sender_id");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.ToTable("chats", (string)null);
                });

            modelBuilder.Entity("Chat.Domain.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("UserFriendId")
                        .HasColumnType("integer")
                        .HasColumnName("friend_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserFriendId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("friends", (string)null);
                });

            modelBuilder.Entity("Chat.Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsChanged")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_changed");

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_read");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time");

                    b.HasKey("Id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("Chat.Domain.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 1L, null, null, null, null, null);

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expiry_time");

                    b.HasKey("Id");

                    b.ToTable("tokens", (string)null);
                });

            modelBuilder.Entity("Chat.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 1L, null, null, null, null, null);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("TokenId")
                        .HasColumnType("integer")
                        .HasColumnName("token_id");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("Email");

                    b.HasIndex("TokenId")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Chat.Domain.Chat", b =>
                {
                    b.HasOne("Chat.Domain.Message", "Message")
                        .WithMany("Chats")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Domain.User", "Reciever")
                        .WithMany("RecieverChats")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Domain.User", "Sender")
                        .WithMany("SenderChats")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("Reciever");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Chat.Domain.Friend", b =>
                {
                    b.HasOne("Chat.Domain.User", "UserFriend")
                        .WithMany("Friends")
                        .HasForeignKey("UserFriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Domain.User", "User")
                        .WithOne()
                        .HasForeignKey("Chat.Domain.Friend", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserFriend");
                });

            modelBuilder.Entity("Chat.Domain.User", b =>
                {
                    b.HasOne("Chat.Domain.Token", "Token")
                        .WithOne("User")
                        .HasForeignKey("Chat.Domain.User", "TokenId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Chat.Domain.Message", b =>
                {
                    b.Navigation("Chats");
                });

            modelBuilder.Entity("Chat.Domain.Token", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat.Domain.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("RecieverChats");

                    b.Navigation("SenderChats");
                });
#pragma warning restore 612, 618
        }
    }
}